#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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