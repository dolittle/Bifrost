using System;
using Bifrost.Samples.Blog.Events.Posts;
using Bifrost.Time;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Views.Specs.Posts.for_PostStatisticsSubscriber
{
    public class when_a_post_is_published : given.a_post_statistics_subscriber
    {
        static int year = 2011;
        static int month = 1;
        private static Guid PostId;

        Because of = () =>
                         {
                             PostId = Guid.NewGuid();
                             PostCreated postCreated;

                             using(SystemClock.SetNowTo(new DateTime(year,month,1)))
                             {
                                 postCreated = new PostCreated(PostId, Guid.NewGuid(), "Something");
                             }
                             subscriber.Process(postCreated);
                         };

        It should_update_statistics_for_the_year_and_month_it_was_published = 
			() => post_statistics_mock.Verify(ss => ss.AggregateForMonth(year, month, new[] {PostId}));
    }
}
