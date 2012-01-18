using System.Web.Mvc;
using System.Web.Security;
using Bifrost.Samples.Blog.Application.Security;

namespace Bifrost.Samples.Blog.Mvc.Features.Users
{
    public class UsersController : Controller
    {
        readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            
        }


        public ActionResult LogOn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string userName, string password, string redirectUrl)
        {
            var loggedIn = _authenticationService.Login(userName, password);
            if( loggedIn )
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return Redirect(redirectUrl);
            } else
            {
                return RedirectToAction("LogOn");
            }
        }
    }
}