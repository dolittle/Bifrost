/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Specialized;
using System.IO;

namespace Bifrost.Web.Services
 {
     /// <summary>
     /// Defines a HTTP request
     /// </summary>
     public interface IHttpRequest
     {
        /// <summary>
        /// Gets the querystring as a <see cref="NameValueCollection">name / value collection</see>
        /// </summary>
        NameValueCollection QueryString { get; }

        /// <summary>
        /// Gets the form as a <see cref="NameValueCollection">name / value collection</see>
        /// </summary>
        NameValueCollection Form { get; }

        /// <summary>
        /// Gets the <see cref="Stream"/> representing the input
        /// </summary>
        Stream InputStream { get; }
     }
 }