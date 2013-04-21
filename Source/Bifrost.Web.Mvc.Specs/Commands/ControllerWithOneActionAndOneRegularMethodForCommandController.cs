using System.Web.Mvc;
using Bifrost.Testing.Fakes.Commands;

namespace Bifrost.Web.Mvc.Specs.Commands
{
    public class ControllerWithOneActionAndOneRegularMethodForCommandController : Controller
    {
        public ActionResult DoStuff(SimpleCommand command)
        {
            return View();
        }

        public void DoMoreStuff(SimpleCommand command)
        {
        }
    }
}
