using System;
using Bifrost.Domain;
using Bifrost.Samples.Blog.Events.Security;

namespace Bifrost.Samples.Blog.Domain.Security
{
    public class User : AggregatedRoot
    {
        string _password;

        public User(Guid id) : base(id)
        {
        }


        public void Create(string userName)
        {
            Apply(new UserCreated(Id, userName, string.Empty));
            SetUserName(userName);
        }

        public void SetPassword(string password)
        {
            Apply(new PasswordSet(Id, password));
        }

        public void Login()
        {
            Apply(new LoginSuccessful(Id));
        }
        

        private void SetUserName(string userName)
        {
            Apply(new UserNameSet(Id, userName));
        }

        public void Handle(PasswordSet passwordSet)
        {
            _password = passwordSet.Password;
        }

        public void Handle(UserCreated userCreated)
        {
            _password = userCreated.Password;
        }

        public void Handle(UserNameSet userNameSet) {}
        public void Handle(LoginSuccessful loginSuccessful) {}
        public void Handle(LoginAttemptedWithWrongPassword loginAttemptedWithWrongPassword) {}
    }
}