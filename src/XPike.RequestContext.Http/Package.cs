using XPike.IoC;

namespace XPike.RequestContext.Http
{
    /// <inheritdoc cref="IDependencyPackage" />
    /// <summary>
    /// Configures the Dependency Injection container with the necessary
    /// registrations for XPike.DefaultRequestContext.Http.
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        /// <summary>
        /// Registers the XPike.DefaultRequestContext dependencies in the DI container.
        /// </summary>
        /// <param name="dependencyCollection">The IDependencyCollection to add registrations to.</param>
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            /* Add necessary registrations from XPike.DefaultRequestContext */
            dependencyCollection.LoadPackage(new XPike.RequestContext.Package());

            /* Add IHttpRequestContextProvider to the collection */
            // NOTE: It is expected that an implementing library will also register as a singleton an implementation of IHttpRequestContextProvider.
            dependencyCollection.AddSingletonToCollection<IRequestContextProvider, IHttpRequestContextProvider>(container =>
                    container.ResolveDependency<IHttpRequestContextProvider>());
        }
    }
}