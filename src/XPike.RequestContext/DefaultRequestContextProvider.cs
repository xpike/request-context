using System;
using System.Collections.Generic;

namespace XPike.RequestContext
{
    /// <inheritdoc cref="IDefaultRequestContextProvider" />
    /// <summary>
    /// Returns an empty DefaultRequestContext.
    /// </summary>
    public class DefaultRequestContextProvider
        : IDefaultRequestContextProvider
    {
        private const string _DEFAULT_STRING_VALUE = "None";
        private const int _DEFAULT_PORT = 0;

        /// <inheritdoc />
        /// <summary>
        /// Creates a new RequestContext with all "empty" values.
        /// 
        /// The RequestId will be set to a new Guid.
        /// Dictionaries will be non-null, but empty.
        /// Strings will have the value "None".
        /// Port will have a value of 0.
        /// </summary>
        public IRequestContext CreateContext() =>
            new RequestContext
            {
                RequestId = Guid.NewGuid(),
                Headers = new Dictionary<string, string>(),
                Protocol = _DEFAULT_STRING_VALUE,
                Verb = _DEFAULT_STRING_VALUE,
                Host = _DEFAULT_STRING_VALUE,
                Port = _DEFAULT_PORT,
                Address = _DEFAULT_STRING_VALUE,
                Parameters = new Dictionary<string, string>()
            };
    }
}