using XPike.IoC;

namespace XPike.RequestContext
{
    /// <summary>
    /// Extension methods for registering and configuring base support for XPike.DefaultRequestContext.
    /// </summary>
    public static class IDependencyCollectionExtensions
    {
        /// <summary>
        /// Adds the XPike.DefaultRequestContext library to the DI container.
        /// You should use one of the extension methods from an implementing library instead, such as from XPike.DefaultRequestContext.Http.AspNetCore.
        /// 
        /// NOTE: If you call this directly, you should also add an appropriate implementation of IRequestContextProvider to a Singleton Collection.
        /// </summary>
        /// <param name="collection">The IDependencyCollection to add registrations to.</param>
        /// <returns>The IDependencyCollection.</returns>
        public static IDependencyCollection AddXPikeRequestContext(this IDependencyCollection collection) =>
            collection.LoadPackage(new XPike.RequestContext.Package());
    }
}