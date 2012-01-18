using Bifrost.Domain;
using Bifrost.Samples.Blog.Domain.Posts;
using Bifrost.Samples.Blog.Domain.Posts.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Samples.Blog.Domain.Specs.Posts.for_Post_command_handlers.given
{
    public class a_post_command_handler
    {
        protected static Mock<IAggregatedRootFactory<Post>> factory_mock;
        protected static Mock<IAggregatedRootRepository<Post>> repository_mock;
        protected static PostCommandHandler command_handler;

        Establish context = () =>
                                {
                                    factory_mock = new Mock<IAggregatedRootFactory<Post>>();
                                    command_handler = new PostCommandHandler(factory_mock.Object, repository_mock.Object);
                                };
    }
}
