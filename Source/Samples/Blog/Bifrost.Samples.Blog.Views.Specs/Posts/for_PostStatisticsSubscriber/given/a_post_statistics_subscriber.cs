using Bifrost.Samples.Blog.Views.Posts;
using Machine.Specifications;
using Moq;

namespace Bifrost.Samples.Blog.Views.Specs.Posts.for_PostStatisticsSubscriber.given
{
    public class a_post_statistics_subscriber
    {
        protected static PostStatisticsSubscriber subscriber;
        protected static Mock<IPostStatisticsService> post_statistics_mock;

        Establish context = () =>
                                {
                                    post_statistics_mock = new Mock<IPostStatisticsService>();
                                    subscriber = new PostStatisticsSubscriber(post_statistics_mock.Object);
                                };
    }
}
