using XPike.IoC;

namespace XPike.RequestContext.Http
{
    /// <summary>
    /// Extension methods to register and configure XPike.DefaultRequestContext.Http.
    /// </summary>
    public static class IDependencyCollectionExtensions
    {
        /// <summary>
        /// Adds dependencies for XPike.DefaultRequestContext.Http to the DI container.
        /// You should use one of the extension methods from an implementing library instead, such as from XPike.DefaultRequestContext.Http.AspNetCore.
        /// 
        /// NOTE: If you call this directly, you should also add an appropriate implementation of IRequestContextProvider to a Singleton Collection.
        /// </summary>
        /// <param name="collection">The IDependencyCollection to add registrations to.</param>
        /// <returns>The IDependencyCollection.</returns>
        public static IDependencyCollection AddXPikeHttpRequestContext(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.RequestContext.Http.Package());
    }
}