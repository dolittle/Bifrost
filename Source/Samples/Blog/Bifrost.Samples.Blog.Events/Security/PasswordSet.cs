using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Security
{
    public class PasswordSet : Event
    {
        public PasswordSet(Guid userId, string password)
            : base(userId)
        {
            Password = password;
        }

        public string Password { get; set; }
    }
}