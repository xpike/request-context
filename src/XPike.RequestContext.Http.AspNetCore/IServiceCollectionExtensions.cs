using Microsoft.Extensions.DependencyInjection;
using XPike.IoC;
using XPike.IoC.Microsoft;

namespace XPike.RequestContext.Http.AspNetCore
{
    /// <summary>
    /// Extension methods to register and configure XPike.RequestContext.Http.AspNetCore.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Registers XPike.RequestContext.Http.AspNetCore with the DI provider.
        /// 
        /// NOTE: Also calls IServiceCollection.AddHttpContextAccessor(), which is a dependency.
        /// </summary>
        /// <param name="services">The IServiceCollection to register with.</param>
        /// <returns>The IServiceCollection.</returns>
        public static IServiceCollection AddXPikeHttpRequestContext(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            new MicrosoftDependencyCollection(services).LoadPackage(new XPike.RequestContext.Http.AspNetCore.Package());

            return services;
        }
    }
}