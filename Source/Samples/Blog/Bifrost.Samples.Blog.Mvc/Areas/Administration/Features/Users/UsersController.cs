using System.Web.Mvc;
using Bifrost.Commands;
using Bifrost.Samples.Blog.Application.Security;
using Bifrost.Samples.Blog.Domain.Security.Commands;

namespace Bifrost.Samples.Blog.Mvc.Areas.Administration.Features.Users
{
    public class UsersController : AdministrationController
    {
        readonly IAuthenticationService _authenticationService;
        readonly ICommandCoordinator _commandCoordinator;

        public UsersController(IAuthenticationService authenticationService, ICommandCoordinator commandCoordinator)
        {
            _authenticationService = authenticationService;
            _commandCoordinator = commandCoordinator;
        }

        public ActionResult Index()
        {
            var users = _authenticationService.GetAllUsers();
            return View(users);
        }


        public ActionResult Create(CreateUser createUser)
        {
            _commandCoordinator.Handle(createUser);
            return RedirectToAction("Show",new {userName=createUser.Name} );
        }


        public ActionResult Show(string userName)
        {
            var user = _authenticationService.GetUserByName(userName);
            return View(user);
        }

    }
}