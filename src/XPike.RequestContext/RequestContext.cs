using System;
using System.Collections.Generic;

namespace XPike.RequestContext
{
    /// <summary>
    /// The default implementation of IRequestContext.
    /// </summary>
    public class RequestContext
        : IRequestContext
    {
        /// <inheritdoc />
        public virtual Guid RequestId { get; set; }

        /// <inheritdoc />
        public virtual IDictionary<string, string> Headers { get; set; }

        /// <inheritdoc />
        public virtual string Protocol { get; set; }

        /// <inheritdoc />
        public virtual string Verb { get; set; }

        /// <inheritdoc />
        public virtual string Host { get; set; }

        /// <inheritdoc />
        public virtual int Port { get; set; }

        /// <inheritdoc />
        public virtual string Address { get; set; }

        /// <inheritdoc />
        public virtual IDictionary<string, string> Parameters { get; set; }
    }
}