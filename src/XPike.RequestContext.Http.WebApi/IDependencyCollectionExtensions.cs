using XPike.IoC;

namespace XPike.RequestContext.Http.WebApi
{
    /// <summary>
    /// Extension methods for registering and configuring XPike.RequestContext.Http.WebApi.
    /// </summary>
    public static class IDependencyCollectionExtensions
    {
        /// <summary>
        /// Registers XPike.RequestContext.Http.WebApi with the DI provider.
        /// </summary>
        /// <param name="collection">The IDependencyCollection to register with.</param>
        /// <returns>The IDependencyCollection.</returns>
        public static IDependencyCollection AddXPikeWebApiRequestContext(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.RequestContext.Http.WebApi.Package());
    }
}