﻿using System;
using System.Collections.Generic;

namespace XPike.RequestContext
{
    /// <summary>
    /// A protocol-agnostic representation of the concept of a Request Context.
    /// 
    /// At a later time we may introduce a subclass of this interface which also contains
    /// the payload, such as the string body of an HTTP POST, or a generic TPayload object.
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// A unique ID representing this request.
        /// This is intended to be generated by the server each time a request is received.
        /// It therefore can't be relied upon as a nonce for de-duplication.
        /// </summary>
        Guid RequestId { get; }

        /// <summary>
        /// Additional data about the request represented in some form of header construct
        /// such as HTTP Request Headers for a WebAPI/MVC request.
        /// 
        /// If a header has multiple values, they should be separated by ";".
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// The protocol used to transmit the request, such as "http" or "https".
        /// </summary>
        string Protocol { get; }

        /// <summary>
        /// The verb used to define how an action is called, such as "GET" or "POST" for HTTP requests.
        /// </summary>
        string Verb { get; }

        /// <summary>
        /// The hostname or IP address (if available/applicable) used to transmit the request, or "None".
        /// </summary>
        string Host { get; }

        /// <summary>
        /// The TCP port (if available/applicable) where the request was transmitted to, or 0.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// The "address" the request was targeting, such as a URL like "/user/3/details" for HTTP or a topic name for AMQP.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Any parameters specific to the request which are separate from the request's payload,
        /// such as QueryString parameters for an HTTP request.
        ///
        /// If a parameter has multiple values, they should be separated by ";".
        /// </summary>
        IDictionary<string, string> Parameters { get; }

        /// <summary>
        /// A list of claims for the current request context.
        /// Presumably sourced from something such as HttpContext.User / JWT.
        /// </summary>
        IDictionary<string, string> Claims { get; }
    }
}