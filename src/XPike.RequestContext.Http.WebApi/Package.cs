using XPike.IoC;

namespace XPike.RequestContext.Http.WebApi
{
    /// <inheritdoc cref="IDependencyPackage" />
    /// <summary>
    /// DI package for XPike.RequestContext.Http.WebApi.
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        /// <summary>
        /// Registers XPike.RequestContext.Http.WebApi with the DI provider.
        /// </summary>
        /// <param name="dependencyCollection">The IDependencyCollection to register with.</param>
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            /* Load the XPike.RequestContext.Http package which we depend on */
            dependencyCollection.LoadPackage(new XPike.RequestContext.Http.Package());

            dependencyCollection.RegisterSingleton<IHttpRequestContextProvider, WebApiRequestContextProvider>();
        }
    }
}