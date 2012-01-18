using Bifrost.Commands;
using Bifrost.Domain;

namespace Bifrost.Samples.Blog.Domain.Security.Commands
{
    public class UserCommandHandler : ICommandHandler
    {
        readonly IAggregatedRootFactory<User> _factory;
        readonly IAggregatedRootRepository<User> _repository;

        public UserCommandHandler(IAggregatedRootFactory<User> factory, IAggregatedRootRepository<User> repository)
        {
            _factory = factory;
            _repository = repository;
        }


        public void Handle(CreateUser createUser)
        {
            var user = _factory.Create(createUser.Id);
            user.Create(createUser.Name);
            user.SetPassword(createUser.Password);
        }

        public void Handle(SetPassword setPassword)
        {
        }

        public void Handle(LoginUser loginUser)
        {
            var user = _repository.Get(loginUser.Id);
            user.Login();
        }
    }
}
