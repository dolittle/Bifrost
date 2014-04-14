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
using System.Web;
using Bifrost.Execution;
namespace Bifrost.Web
{
    public class WebCallContext : ICallContext
    {
        public bool HasData(string key)
        {
            return HttpContext.Current.Items.Contains(key);
        }

        public T GetData<T>(string key)
        {
            return (T)HttpContext.Current.Items[key];
        }

        public void SetData(string key, object data)
        {
            HttpContext.Current.Items[key] = data;
        }
    }
}
