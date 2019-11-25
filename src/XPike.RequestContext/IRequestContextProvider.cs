namespace XPike.RequestContext
{
    /// <summary>
    /// Represents a Provider of Request Context data which may return a Request Context
    /// if it is authoritative over the current processing scope, or null otherwise.
    ///
    /// This should be added to a Singleton Collection in the DI container.
    /// </summary>
    public interface IRequestContextProvider
    {
        /// <summary>
        /// Creates and returns a new Request Context, if this provider is authoritative
        /// over the current processing scope.
        /// </summary>
        /// <returns>The current DefaultRequestContext, or null.</returns>
        IRequestContext CreateContext();
    }
}