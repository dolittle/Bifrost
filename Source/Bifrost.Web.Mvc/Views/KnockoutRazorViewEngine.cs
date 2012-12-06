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
