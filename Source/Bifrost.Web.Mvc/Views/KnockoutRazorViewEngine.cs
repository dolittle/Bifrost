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
using System.Web.Mvc;
using System.IO;

namespace Bifrost.Web.Mvc.Views
{
    public class KnockoutRazorViewEngine : RazorViewEngine
    {
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var result = base.FindPartialView(controllerContext, partialViewName, useCache);
            if (result.View == null)
                result = GetPartialViewByConvention(controllerContext, partialViewName, useCache) ?? result;
            return result;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var result = base.FindView(controllerContext, viewName, masterName, useCache);
            if (result.View == null)
                result = GetViewByConvention(controllerContext, viewName, useCache) ?? result;
            return result;
        }

        ViewEngineResult GetPartialViewByConvention(ControllerContext controllerContext, string viewName, bool useCache)
        {
            return GetViewByConventionImpl(controllerContext, viewName, useCache, true, false);
        }

        ViewEngineResult GetViewByConvention(ControllerContext controllerContext, string viewName, bool useCache)
        {
            return GetViewByConventionImpl(controllerContext, viewName, useCache, false, true);
        }


        ViewEngineResult GetViewByConventionImpl(ControllerContext controllerContext, string viewName, bool useCache, bool isPartial, bool runViewStartPages)
        {
            if (KnockoutRazorView.ViewExists(controllerContext.HttpContext, viewName, controllerContext.RouteData))
            {
                var view = new KnockoutRazorView(controllerContext, viewName, string.Empty, isPartial, runViewStartPages, new[] { "cshtml" });
                return new ViewEngineResult(view, this);
            }
            return null;
        }

    }
}
