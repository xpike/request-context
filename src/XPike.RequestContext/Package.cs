using XPike.IoC;

namespace XPike.RequestContext
{
    /// <inheritdoc cref="IDependencyPackage" />
    /// <summary>
    /// Configures the Dependency Injection container with the necessary
    /// registrations for XPike.DefaultRequestContext.
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
            /* Load the XPike.Logging package which we depend on. */
            dependencyCollection.LoadPackage(new XPike.Logging.Package());

            /* Register Singletons */
            dependencyCollection.RegisterSingleton<IDefaultRequestContextProvider, DefaultRequestContextProvider>();
            dependencyCollection.RegisterSingleton<IRequestContextAccessor, RequestContextAccessor>();

            /* Add Default Request Context Provider */
            dependencyCollection.AddSingletonToCollection<IRequestContextProvider, IDefaultRequestContextProvider>(container =>
                    container.ResolveDependency<IDefaultRequestContextProvider>());

            /* Add accessor for scoped injection of IRequestContext */
            dependencyCollection.RegisterScoped<IRequestContext>(services =>
                services.ResolveDependency<IRequestContextAccessor>().RequestContext);
        }
    }
}