using System.Web.Mvc;

namespace Bifrost.Samples.Blog.Mvc.Areas.Administration
{
    [Authorize(Users="Admin")]
    public class AdministrationController : Controller
    {
    }
}