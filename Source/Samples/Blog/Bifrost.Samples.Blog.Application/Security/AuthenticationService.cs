using System.Collections.Generic;
using System.Linq;
using Bifrost.Commands;
using Bifrost.Views;
using Bifrost.Samples.Blog.Domain.Security.Commands;
using Bifrost.Samples.Blog.Views.Security;

namespace Bifrost.Samples.Blog.Application.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly IView<User> _repository;
        readonly ICommandCoordinator _commandCoordinator;

        public AuthenticationService(IView<User> repository, ICommandCoordinator commandCoordinator)
        {
            _repository = repository;
            _commandCoordinator = commandCoordinator;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _repository.Query.ToArray();
            return users;
        }

        public User GetUserByName(string userName)
        {
            var query = from u in _repository.Query
                        where u.UserName == userName
                        select u;

            var user = query.SingleOrDefault();
            return user;
        }

        public bool Login(string userName, string password)
        {
            var query = from u in _repository.Query
                        where u.UserName == userName && u.Password == password
                        select u;

            var user = query.SingleOrDefault();

            if( null != user )
            {
                var loginUser = new LoginUser {Id = user.Id};
                _commandCoordinator.Handle(loginUser);
            }

            return user != null;
        }
    }
}