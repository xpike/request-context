using XPike.IoC;

namespace XPike.RequestContext.Http.AspNetCore
{
    /// <inheritdoc cref="IDependencyPackage" />
    /// <summary>
    /// DI registration package for XPike.RequestContext.Http.AspNetCore
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        /// <inheritdoc />
        /// <summary>
        /// Registers XPike.RequestContext.Http.AspNetCore with the DI provider.
        /// </summary>
        /// <param name="dependencyCollection">The IDependencyCollection to register with.</param>
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            /* Include XPike.RequestContext.Http registrations which we depend on */
            dependencyCollection.LoadPackage(new XPike.RequestContext.Http.Package());

            /* Register the ASP.NET Core Request Context Provider */
            dependencyCollection.RegisterSingleton<IAspNetCoreRequestContextProvider, AspNetCoreRequestContextProvider>();

            /* Re-Map IHttpRequestContextProvider to use IAspNetCoreRequestContextProvider */
            dependencyCollection.RegisterSingleton<IHttpRequestContextProvider>(container =>
                container.ResolveDependency<IAspNetCoreRequestContextProvider>());
        }
    }
}