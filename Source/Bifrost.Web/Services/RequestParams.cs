#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using System.Collections.Specialized;
using System.Web;

namespace Bifrost.Web.Services
{
    /// <summary>
    /// Encapsulates a collection of parameters from the Http Request
    /// </summary>
    public class RequestParams : NameValueCollection
    {
        /// <summary>
        /// Adds a new Parameter to the collection
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="value">Value</param>
        public override void Add(string name, string value)
        {
            if(this[name] == null)
                base.Add(name, value);
        }

        /// <summary>
        /// Converts an HttpCookieCollection into parameters
        /// </summary>
        /// <param name="cookies">An HttpCookieCollection</param>
        public void Add(HttpCookieCollection cookies)
        {
            var count = cookies.Count;
            for(var index = 0; index < count; index++)
            {
                var cookie = cookies.Get(index);
                Add(cookie.Name, cookie.Value);
            }
        }
    }
}