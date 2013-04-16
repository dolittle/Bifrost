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
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bifrost.Web.Mvc.Views
{
    public class KnockoutRazorView : RazorView
    {
        string _viewName;
        bool _isPartial;

        public KnockoutRazorView(
            ControllerContext controllerContext,
            string viewName,
            string layoutPath,
            bool isPartial,
            bool runViewStartPages,
            IEnumerable<string> viewStartFileExtensions)
            : base(controllerContext, GetPathFromRoute(viewName, "cshtml", controllerContext.RouteData), layoutPath, runViewStartPages, viewStartFileExtensions)
        {
            _isPartial = isPartial;
            _viewName = viewName;
        }

        public static bool ViewExists(HttpContextBase httpContext, string viewName, RouteData routeData)
        {
            var relativePath = GetPathFromRoute(viewName, "cshtml", routeData);
            var path = httpContext.Server.MapPath(relativePath);
            return File.Exists(path);
        }

        static string GetPathFromRoute(string name, string extension, RouteData routeData)
        {
            var relativePath = string.Format("{0}/{1}.{2}",
                GetFeaturePathFrom(routeData),
                name,
                extension);
            return relativePath;
        }

        static string GetFeaturePathFrom(RouteData routeData)
        {
            var builder = new StringBuilder();
            builder.Append("/Contexts");
            AppendRouteValueIfExist("boundedContext", routeData, builder);
            AppendRouteValueIfExist("module", routeData, builder);
            AppendRouteValueIfExist("feature", routeData, builder);
            return builder.ToString();                
        }

        private static void AppendRouteValueIfExist(string routeValue, RouteData routeData, StringBuilder builder)
        {
            if (routeData.Values[routeValue] != null &&
                !string.IsNullOrEmpty((string)routeData.Values[routeValue])) builder.AppendFormat("/{0}", routeData.Values[routeValue]);
        }

        static string GetNamespaceFrom(RouteData routeData)
        {
            return string.Format("{0}.{1}.{2}",
                routeData.Values["boundedContext"],
                routeData.Values["module"],
                routeData.Values["feature"]);
        }

        string GetViewModelDivStart(string divTagId, string viewModelRelativePath)
        {
            return string.Format("<div id=\"{0}\" data-viewmodel=\"{1}\">",
                    divTagId,
                    viewModelRelativePath);
        }


        protected override void RenderView(ViewContext viewContext, TextWriter writer, object instance)
        {
            var featureRelativePath = GetFeaturePathFrom(viewContext.RouteData);
            var featurePath = viewContext.HttpContext.Server.MapPath(featureRelativePath);
            var featureNamespace = GetNamespaceFrom(viewContext.RouteData);
            var viewModelRelativePath = GetPathFromRoute(_viewName, "js", viewContext.RouteData);
            var viewModelPath = viewContext.HttpContext.Server.MapPath(viewModelRelativePath);
            var hasViewModel = File.Exists(viewModelPath);

            if (!hasViewModel)
            {
                base.RenderView(viewContext, writer, instance);
                return;
            }

            var divTagId = string.Format("{0}_ViewModel", _viewName);

            if (_isPartial)
                RenderPartial(viewContext, writer, instance, viewModelRelativePath, hasViewModel, divTagId);
            else
            {
                var viewStringBuilder = new StringBuilder();
                var viewStringWriter = new StringWriter(viewStringBuilder);
                base.RenderView(viewContext, viewStringWriter, instance);

                var html = viewStringBuilder.ToString();
                var stringBuilder = new StringBuilder();

                var bodyIndex = html.IndexOf("<body");
                var endOfBodyIndex = html.IndexOf(">", bodyIndex);
                var closeBodyIndex = html.IndexOf("</body>");

                stringBuilder.Append(html.Substring(0, endOfBodyIndex + 1));

                stringBuilder.Append(GetViewModelDivStart(divTagId, viewModelRelativePath));

                stringBuilder.Append(html.Substring(endOfBodyIndex + 1, closeBodyIndex - endOfBodyIndex - 1));
                stringBuilder.Append("</div>");
                stringBuilder.Append(html.Substring(closeBodyIndex));

                writer.Write(stringBuilder.ToString());
            }
        }

        void RenderPartial(ViewContext viewContext, TextWriter writer, object instance, string viewModelRelativePath, bool hasViewModel, string divTagId)
        {
            if (hasViewModel)
                writer.WriteLine(GetViewModelDivStart(divTagId, viewModelRelativePath));

            base.RenderView(viewContext, writer, instance);

            if (hasViewModel)
                writer.WriteLine("</div>");
        }


    }
}
