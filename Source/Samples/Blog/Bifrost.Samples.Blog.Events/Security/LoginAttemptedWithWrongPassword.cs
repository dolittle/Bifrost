using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Security
{
    public class LoginAttemptedWithWrongPassword : Event
    {
        public LoginAttemptedWithWrongPassword(Guid userId, string password) : base(userId)
        {
            Password = password;
        }

        public string Password { get; set; }
    }
}
