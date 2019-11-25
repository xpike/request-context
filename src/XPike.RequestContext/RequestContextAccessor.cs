using System;
using System.Collections.Generic;
using System.Linq;
using XPike.Logging;
#if NETSTD
using System.Threading;
#elif NETFX
using System.Runtime.Remoting.Messaging;
#endif

namespace XPike.RequestContext
{
    /// <inheritdoc cref="IRequestContextAccessor" />
    /// <summary>
    /// The default implementation of IRequestContextAccessor.
    ///
    /// Uses an injected collection of Singleton IRequestContextProvider objects, in reverse
    /// order, to create the IRequestContext for the current processing scope.
    /// 
    /// In .NET Core, uses AsyncLocal to cache the IRequestContext.
    /// In .NET Framework, uses CallContext.LogicalSetData() / CallContext.LogicalGetData().
    /// </summary>
    public class RequestContextAccessor
        : IRequestContextAccessor
    {
#if NETSTD

        /// <summary>
        /// Uses AsyncLocal to provide scope-isolated caching of the IRequestContext.
        /// </summary>
        protected static readonly AsyncLocal<IRequestContext> Localizer = new AsyncLocal<IRequestContext>();

#endif

        /// <summary>
        /// The ILog instance to use for logging.
        /// </summary>
        protected readonly ILog<RequestContextAccessor> Logger;

        /// <summary>
        /// The list of IRequestContextProviders to use when creating an IRequestContext.
        /// </summary>
        protected readonly IList<IRequestContextProvider> Providers;

#if NETSTD

        /// <inheritdoc />
        /// <summary>
        /// NOTE: Beware overriding this - it is within this property accessor that the scope isolation through Localizer occurs!
        /// </summary>
        public virtual IRequestContext RequestContext =>
            Localizer.Value ?? (Localizer.Value = CreateContext());

#elif NETFX

        /// <inheritdoc />
        /// <summary>
        /// NOTE: Beware overriding this - it is within this property accessor that the scope isolation with LogicalCallContext occurs!
        /// </summary>
        public virtual IRequestContext RequestContext
        {
            get
            {
                var context = (IRequestContext) CallContext.LogicalGetData(GetType().ToString());

                if (context == null)
                {
                    context = CreateContext();
                    CallContext.LogicalSetData(GetType().ToString(), context);
                }

                return context;
            }
        }

#endif

        /// <summary>
        /// Creates a new RequestContextAccessor which will use the specified IRequestContextProviders
        /// (in reverse order) to find the appropriate IRequestContext for the execution scope when
        /// one is requested.
        /// </summary>
        /// <param name="providers">An IEnumerable of IRequestContextProviders to use.  A reverse-ordered list will be created from this.</param>
        /// <param name="logger">The ILog instance to use for logging.</param>
        public RequestContextAccessor(IEnumerable<IRequestContextProvider> providers, ILog<RequestContextAccessor> logger)
        {
            Logger = logger;
            Providers = providers.Reverse().ToList();
        }

        /// <summary>
        /// Creates the IRequestContext by scanning through the Providers.
        /// The value from the first Provider which returns a non-null IRequestContext will be used.
        ///
        /// A Warning will be logged if no Provider returns a value.
        /// </summary>
        /// <returns>The IRequestContext for the current processing scope.</returns>
        protected virtual IRequestContext CreateContext()
        {
            IRequestContext context = null;

            foreach (var provider in Providers)
            {
                try
                {
                    context = provider.CreateContext();

                    if (context != null)
                        return context;
                }
                catch (Exception ex)
                {
                    Logger.Trace($"Failed to load Request Context from Provider {provider.GetType()}: {ex.Message} ({ex.GetType()})");
                }
            }

            if (context == null)
                Logger.Warn($"Failed to load Request Context from any Provider!");

            return context;
        }
    }
}