using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Web;
using XPike.Logging;

namespace XPike.RequestContext.Http.WebApi
{
    /// <inheritdoc cref="IHttpRequestContextProvider" />
    /// <summary>
    /// Provides a RequestContext when called during processing of an ASP.NET request.
    /// Uses HttpContext.Current to retrieve the HttpContext instance that is in-scope for the current request.
    /// </summary>
    public class WebApiRequestContextProvider
        : IWebApiRequestContextProvider
    {
        private readonly ILog<WebApiRequestContextProvider> _logger;

        /// <summary>
        /// Creates a new WebApiCoreRequestContextProvider.
        /// </summary>
        /// <param name="logger">The ILog instance to use for logging.</param>
        public WebApiRequestContextProvider(ILog<WebApiRequestContextProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Helper to convert from a NameValueCollection to a Dictionary.
        /// </summary>
        /// <param name="nvc"></param>
        /// <returns></returns>
        private static IDictionary<string, string> CreateDictionary(NameValueCollection nvc)
        {
            var dict = new Dictionary<string, string>();
            
            foreach (var key in nvc.AllKeys)
                dict[key] = nvc[key];
            
            return dict;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new RequestContext when called from within an ASP.NET HTTP Request,
        /// or null if no HttpContext can be obtained.
        /// </summary>
        /// <returns>The RequestContext for the current request, or null.</returns>
        public virtual IRequestContext CreateContext()
        {
            IRequestContext context = null;

            try
            {
                var httpContext = HttpContext.Current;

                if (httpContext != null)
                    context = new RequestContext
                    {
                        RequestId = Guid.NewGuid(),
                        Protocol = httpContext.Request.Url.Scheme,
                        Address = httpContext.Request.Url.AbsolutePath,
                        Parameters = CreateDictionary(httpContext.Request.QueryString),
                        Host = httpContext.Request.Url.Host,
                        Verb = httpContext.Request.HttpMethod,
                        Port = httpContext.Request.Url.Port,
                        Headers = CreateDictionary(httpContext.Request.Headers),
                        Claims = (httpContext.User as ClaimsPrincipal)
                                 ?.Claims
                                 ?.ToDictionary(x => x.Type, x => x.Value) ??
                                 new Dictionary<string, string>()
                    };
            }
            catch (Exception ex)
            {
                _logger.Trace($"Failed to load RequestContext from HTTP Context: {ex.Message} ({ex.GetType()})");
            }

            return context;
        }
    }
}