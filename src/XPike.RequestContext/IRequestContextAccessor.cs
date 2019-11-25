namespace XPike.RequestContext
{
    /// <summary>
    /// Represents a Request Context Accessor which is used to identify and retrieve
    /// the Request Context corresponding to the current processing scope.
    ///
    /// This should be registered as a Singleton.
    /// </summary>
    public interface IRequestContextAccessor
    {
        /// <summary>
        /// Retrieves the Request Context for the current processing scope.
        /// </summary>
        IRequestContext RequestContext { get; }
    }
}