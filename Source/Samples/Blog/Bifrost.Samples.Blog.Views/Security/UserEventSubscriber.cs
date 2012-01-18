using Bifrost.Events;
using Bifrost.Samples.Blog.Events.Security;

namespace Bifrost.Samples.Blog.Views.Security
{
    public class UserEventSubscriber : EventSubscriber<User>
    {

        public void Process(UserCreated userCreated)
        {
            var user = new User
                           {
                               Id = userCreated.Id,
                               UserName = userCreated.UserName,
                               Password = userCreated.Password
                           };
            InsertEntity(user);
        }

        public void Process(PasswordSet passwordSet)
        {
            UpdateProperty(passwordSet, u => u.Password = passwordSet.Password);
        }
    }
}
