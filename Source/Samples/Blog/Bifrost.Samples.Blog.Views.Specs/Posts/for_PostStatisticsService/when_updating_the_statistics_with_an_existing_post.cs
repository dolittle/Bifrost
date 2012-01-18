using System;
using System.Linq;
using Bifrost.Samples.Blog.Views.Posts;
using Bifrost.Time;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Views.Specs.Posts.for_PostStatisticsService
{
    public class when_updating_the_statistics_with_an_existing_post : given.five_posts_for_january_2011
    {
        static PostsByYear postsByYear;
        static Guid IdOfExistingPost;

        Because of = () =>
                         {
                             IdOfExistingPost = posts_entity_context.Entities.First().Id;
                             post_statistics_service.AggregateForMonth(year, month, IdOfExistingPost);
                             postsByYear = posts_by_year_entity_context.Entities.First();
                         };

        It should_update_the_posts_in_january_2011_to_5 = () =>
                                                              {
                                                                  posts_by_year_entity_context.Entities.Count().ShouldEqual(1);
                                                                  postsByYear.January.ShouldEqual(5);
                                                                  foreach (var post in postsByYear.Counts.Where(c => c.Key != Month.January))
                                                                  {
                                                                      post.Value.ShouldEqual(0);
                                                                  }
                                                              };
    }
}