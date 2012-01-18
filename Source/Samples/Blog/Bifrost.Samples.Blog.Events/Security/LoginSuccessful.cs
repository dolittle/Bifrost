using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Security
{
    public class LoginSuccessful : Event
    {
        public LoginSuccessful(Guid userId) : base(userId) {}
    }
}