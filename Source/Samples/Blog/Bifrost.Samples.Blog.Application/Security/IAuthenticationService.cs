using System.Collections.Generic;
using Bifrost.Samples.Blog.Views.Security;

namespace Bifrost.Samples.Blog.Application.Security
{
    public interface IAuthenticationService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByName(string userName);
        bool Login(string userName, string password);
    }
}