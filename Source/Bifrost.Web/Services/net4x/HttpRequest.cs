/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Specialized;
using System.IO;
using System.Web;

namespace Bifrost.Web.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IHttpRequest"/> for System.Web
    /// </summary>
    public class HttpRequest : IHttpRequest
    {
        HttpRequestWrapper _actualRequest;

        /// <summary>
        /// Initializes a new instance of <see cref="HttpRequest"/>
        /// </summary>
        /// <param name="actualRequest"><see cref="HttpRequestWrapper">Actual request</see></param>
        public HttpRequest(HttpRequestWrapper actualRequest)
        {
            _actualRequest = actualRequest;

        }

        /// <inheritdoc/>
        public NameValueCollection QueryString => _actualRequest.QueryString;

        /// <inheritdoc/>
        public NameValueCollection Form => _actualRequest.Form;

        /// <inheritdoc/>
        public Stream InputStream => _actualRequest.InputStream;
    }
}
