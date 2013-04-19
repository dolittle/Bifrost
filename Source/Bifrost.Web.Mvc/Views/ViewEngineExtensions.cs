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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bifrost.Web.Mvc.Views
{
    public static class ViewEngineExtensions
    {
        /// <summary>
        /// Changes the default convention for locations of Views and Master pages for all view engines
        /// </summary>
        /// <param name="siteRelativePath">Relative path within the site from root of site you want your views to be found</param>
        /// <remarks>
        /// By default ASP.net MVC has its views located in the Views folder of your site.
        /// This method can automatically change that location if one likes.
        /// </remarks>
        public static void RelocateViews(this ViewEngineCollection engines, string siteRelativePath)
        {
            foreach (var viewEngine in engines)
            {
                if (viewEngine is VirtualPathProviderViewEngine)
                {
                    var virtualPathViewEngine = viewEngine as VirtualPathProviderViewEngine;

                    virtualPathViewEngine.MasterLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.MasterLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.ViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.ViewLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.PartialViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.PartialViewLocationFormats, "Views", siteRelativePath);

                    virtualPathViewEngine.AreaMasterLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.AreaMasterLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.AreaViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.AreaViewLocationFormats, "Views", siteRelativePath);
                    virtualPathViewEngine.AreaPartialViewLocationFormats =
                        ReplaceInStrings(virtualPathViewEngine.AreaPartialViewLocationFormats, "Views", siteRelativePath);
                }
            }
        }

        static string[] ReplaceInStrings(IEnumerable<string> strings, string partToReplace, string replaceWith)
        {
            return strings.Select(@string => @string.Replace(partToReplace, replaceWith)).ToArray();
        }

    }
}
