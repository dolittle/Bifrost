using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Security
{
    public class UserCreated : Event
    {
        public UserCreated(Guid userId, string userName, string password) : base(userId)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
