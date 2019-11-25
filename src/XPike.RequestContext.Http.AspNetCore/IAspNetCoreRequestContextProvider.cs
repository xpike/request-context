namespace XPike.RequestContext.Http.AspNetCore
{
    /// <inheritdoc cref="IRequestContextProvider" />
    /// <summary>
    /// A purely decorative/categorical interface to differentiate an
    /// ASP.NET Core Request Context Provider from other types.
    /// </summary>
    public interface IAspNetCoreRequestContextProvider
        : IHttpRequestContextProvider
    {
    }
}