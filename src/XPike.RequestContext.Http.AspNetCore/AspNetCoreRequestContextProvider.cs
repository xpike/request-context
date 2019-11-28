using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using XPike.Logging;

namespace XPike.RequestContext.Http.AspNetCore
{
    /// <inheritdoc cref="IHttpRequestContextProvider" />
    /// <summary>
    /// Provides a RequestContext when called during processing of an ASP.NET Core HTTP request.
    /// 
    /// NOTE: This requires that IServiceCollection.AddHttpContextAccessor() has been called.
    /// </summary>
    public class AspNetCoreRequestContextProvider
        : IAspNetCoreRequestContextProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILog<AspNetCoreRequestContextProvider> _logger;

        /// <summary>
        /// Creates a new AspNetCoreRequestContextProvider using the provided IHttpContextAccessor
        /// to retrieve the HttpContext instance that is in-scope for the current request.
        /// </summary>
        /// <param name="contextAccessor">The IHttpContextAccessor to use to retrieve an HttpContext instance.</param>
        /// <param name="logger">The ILog instance to use for logging.</param>
        public AspNetCoreRequestContextProvider(IHttpContextAccessor contextAccessor, ILog<AspNetCoreRequestContextProvider> logger)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a RequestContext for the currently running ASP.NET Core HTTP
        /// request, or null if no HttpContext could be obtained.
        ///
        /// Headers and QueryString parameters with more than one value will have
        /// those values separated by ";".
        /// </summary>
        /// <returns>The RequestContext for the current request, or null.</returns>
        public virtual IRequestContext CreateContext()
        {
            IRequestContext context = null;

            try
            {
                var httpContext = _contextAccessor.HttpContext;

                if (httpContext != null)
                    context = new RequestContext
                    {
                        RequestId = Guid.NewGuid(),
                        Headers = httpContext.Request
                            .Headers
                            .ToDictionary(x => x.Key,
                                x => string.Join(";", (IEnumerable<string>) x.Value)),
                        Protocol = httpContext.Request.Scheme,
                        Host = httpContext.Request.Host.Host,
                        Port = httpContext.Request
                            .Host
                            .Port
                            .GetValueOrDefault(httpContext.Request.Scheme.ToLower() == "https" ? 443 : 80),
                        Address = httpContext.Request.Path,
                        Parameters = httpContext.Request
                            .Query
                            .ToDictionary(x => x.Key,
                                x => string.Join(";", (IEnumerable<string>) x.Value)),
                        Verb = httpContext.Request.Method,
                        Claims = httpContext.User
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