#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.IO;
using System.Web;
using System.Web.Routing;

namespace Bifrost.Web.Pipeline
{
	public class SinglePageApplication : IPipe
	{
		public void Before (IWebContext webContext)
		{
			if( !HasExtension(webContext) &&
			    !webContext.HasRouteForCurrentRequest ||
                IsDefault(webContext))
				webContext.RewritePath("/index.html");
		}

		public void After (IWebContext webContext)
		{
		}

        bool IsDefault(IWebContext webContext)
        {
            var path = StripLeadingSlashIfAny(webContext.Request.Path);
            return path.Length == 0;
        }

		bool HasExtension(IWebContext webContext)
		{
			var path = webContext.Request.Path;
			if( path.Length > 0 ) 
			{
				if( path.StartsWith("/") ) 
				{
					var extension = Path.GetExtension(path);					
					if( !string.IsNullOrEmpty(extension) ) 
						return true;
				}
			}
			return false;
		}

        string GetPathTillPlaceholdersStartIfAny(string path)
        {
            var index = path.IndexOf('{');
            if (index > 0)
                return path.Substring(0, index);

            return path;
        }
		
		string StripLeadingSlashIfAny(string path)
		{
			if( path.Length > 0 && path.StartsWith("/") ) 
				path = path.Substring(1);
			
			return path;
		}
	}
}

