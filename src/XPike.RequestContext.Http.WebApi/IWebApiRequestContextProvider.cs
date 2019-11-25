namespace XPike.RequestContext.Http.WebApi
{
    /// <inheritdoc cref="IHttpRequestContextProvider" />
    /// <summary>
    /// A purely decorative/categorical interface to differentiate an ASP.NET
    /// Request Context Provider from other types.
    /// </summary>
    public interface IWebApiRequestContextProvider
        : IHttpRequestContextProvider
    {
    }
}