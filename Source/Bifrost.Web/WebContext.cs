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
using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Bifrost.Configuration;
using Bifrost.Web.Configuration;

namespace Bifrost.Web
{
	public class WebContext : IWebContext
	{
		HttpContext _actualHttpContext;
		
		public WebContext (HttpContext actualHttpContext)
		{
			_actualHttpContext = actualHttpContext;
			Request = new Request(actualHttpContext.Request);
			Routes = RouteTable.Routes;
		}

		public void RewritePath (string path)
		{
			_actualHttpContext.RewritePath(path);
		}

		public IWebRequest Request { get; private set; }
		public RouteCollection Routes { get; private set; }


        public bool HasRouteForCurrentRequest
        {
            get 
            {
                return RouteTable.Routes.GetRouteData(new HttpContextWrapper(_actualHttpContext)) != null;
            }
        }
    }
}

