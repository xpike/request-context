namespace XPike.RequestContext.Http
{
    /// <inheritdoc cref="IRequestContextProvider" />
    /// <summary>
    /// A purely decorative/categorical interface to differentiate an HTTP Request
    /// Context Provider from other types of IRequestContextProvider.
    /// </summary>
    public interface IHttpRequestContextProvider
        : IRequestContextProvider
    {
    }
}