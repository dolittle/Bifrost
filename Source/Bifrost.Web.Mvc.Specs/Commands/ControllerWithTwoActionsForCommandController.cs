using System.Web.Mvc;
using Bifrost.Testing.Fakes.Commands;

namespace Bifrost.Web.Mvc.Specs.Commands
{
    public class ControllerWithTwoActionsForCommandController : Controller
    {
        public ActionResult DoStuff(SimpleCommand command)
        {
            return View();
        }

        public ActionResult DoMoreStuff(SimpleCommand command)
        {
            return View();
        }
    }
}
