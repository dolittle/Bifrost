/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Bifrost.Extensions;

namespace Bifrost.Web.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IHttpRequest"/> for System.Web
    /// </summary>
    public class HttpRequest : IHttpRequest
    {
        Microsoft.AspNetCore.Http.HttpRequest _actualRequest;

        /// <summary>
        /// Initializes a new instance of <see cref="HttpRequest"/>
        /// </summary>
        /// <param name="actualRequest"><see cref="HttpRequestWrapper">Actual request</see></param>
        public HttpRequest(Microsoft.AspNetCore.Http.HttpRequest actualRequest)
        {
            _actualRequest = actualRequest;

            QueryString = new NameValueCollection();
            actualRequest.Query.ForEach(kv => QueryString.Add(kv.Key, kv.Value));

            Form = new NameValueCollection();
            if (actualRequest.HasFormContentType) actualRequest.Form.ForEach(kv => Form.Add(kv.Key, kv.Value));
        }

        /// <inheritdoc/>
        public NameValueCollection QueryString { get; }

        /// <inheritdoc/>
        public NameValueCollection Form { get; }

        /// <inheritdoc/>
        public Stream InputStream
        {
            get
            {
                var body = string.Empty;
                using (var reader = new StreamReader(_actualRequest.Body)) body = reader.ReadToEnd();

                var stream = new MemoryStream(Encoding.UTF8.GetBytes(body));
                return stream;
            }
        }
    }
}
