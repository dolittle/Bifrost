using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Security
{
    public class LoginAttemptedWithWrongUserName : Event
    {
        public LoginAttemptedWithWrongUserName(Guid securityLogId) : base(securityLogId) {}
    }
}