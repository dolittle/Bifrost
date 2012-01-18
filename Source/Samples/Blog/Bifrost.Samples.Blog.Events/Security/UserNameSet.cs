using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Security
{
    public class UserNameSet : Event
    {
        public UserNameSet(Guid userId, string userName)
            : base(userId)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}