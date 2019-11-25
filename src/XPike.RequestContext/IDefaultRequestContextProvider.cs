namespace XPike.RequestContext
{
    /// <inheritdoc cref="IRequestContextProvider" />
    /// <summary>
    /// Represents the default implementation of IRequestContextProvider which returns
    /// a default/empty IRequestContext, when no other providers are a match.
    ///
    /// This should be registered as a Singleton.
    /// </summary>
    public interface IDefaultRequestContextProvider
        : IRequestContextProvider
    {
    }
}