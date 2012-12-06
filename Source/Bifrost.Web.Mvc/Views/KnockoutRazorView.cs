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
            var relativePath = string.Format("/Contexts/{0}/{1}/{2}",
                routeData.Values["boundedContext"],
                routeData.Values["module"],
                routeData.Values["feature"]);
            return relativePath;
        }

        static string GetNamespaceFrom(RouteData routeData)
        {
            return string.Format("{0}.{1}.{2}",
                routeData.Values["boundedContext"],
                routeData.Values["module"],
                routeData.Values["feature"]);
        }

        protected override void RenderView(ViewContext viewContext, TextWriter writer, object instance)
        {
            var featureRelativePath = GetFeaturePathFrom(viewContext.RouteData);
            var featurePath = viewContext.HttpContext.Server.MapPath(featureRelativePath);
            var featureNamespace = GetNamespaceFrom(viewContext.RouteData);
            var viewModelRelativePath = GetPathFromRoute(_viewName, "js", viewContext.RouteData);
            var viewModelPath = viewContext.HttpContext.Server.MapPath(viewModelRelativePath);
            var hasViewModel = File.Exists(viewModelPath);
            var divTagId = string.Format("{0}_ViewModel", _viewName);

            if (hasViewModel)
                writer.WriteLine("<div id=\"{0}\" data-viewmodel=\"{1}\">",
                    divTagId,
                    viewModelRelativePath);

            base.RenderView(viewContext, writer, instance);

            if (hasViewModel)
                writer.WriteLine("</div>");
        }
    }
}
