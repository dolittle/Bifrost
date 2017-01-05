/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using Bifrost.Serialization;

namespace Bifrost.Web.Services
{
    /// <summary>
    /// Represents an instance of <see cref="IRequestParamsFactory"/>
    /// </summary>
    public class RequestParamsFactory : IRequestParamsFactory
    {
        readonly ISerializer _serializer;

        public RequestParamsFactory(ISerializer serializer)
        {
            _serializer = serializer;
        }

#pragma warning disable 1591 // Xml Comments
        public RequestParams BuildParamsCollectionFrom(HttpRequestBase request)
        {
            var queystring = request.QueryString;
            var form = request.Form;
            var requestBody = BuildFormFromInputStream(request.InputStream);

            var requestParams = new RequestParams {queystring, form, requestBody }; 

            return requestParams;
        }
#pragma warning restore 1591 // Xml Comments

        NameValueCollection BuildFormFromInputStream(Stream stream)
        {
            var input = new byte[stream.Length];
            stream.Read(input, 0, input.Length);
            var inputAsString = System.Text.Encoding.UTF8.GetString(input);
            var inputDictionary = _serializer.FromJson(typeof(Dictionary<string, string>), inputAsString, null) as Dictionary<string,string>;

            var inputStreamParams = new NameValueCollection();

            if (inputDictionary != null)
            {
                foreach (var key in inputDictionary.Keys)
                    inputStreamParams[key] = inputDictionary[key];
            }
            
            return inputStreamParams;
        }
    }
}