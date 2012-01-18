using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Samples.Blog.Views.Posts;
using Machine.Specifications;

namespace Bifrost.Samples.Blog.Views.Specs.Posts.for_PostStatisticsService.given
{
    public class five_posts_for_january_2011 : a_post_statistics_service
    {
        protected static List<Post> posts;
        protected static PostsByYear posts_by_year;
        protected static int year = 2011;
        protected static int month = 1;

        Establish context = () =>
                                        {
                                            posts = new List<Post>()
                                                        {
                                                            new Post() {Id = Guid.NewGuid(), PublishDate = new DateTime(year,1,1)},
                                                            new Post() {Id = Guid.NewGuid(), PublishDate = new DateTime(year,1,1)},
                                                            new Post() {Id = Guid.NewGuid(), PublishDate = new DateTime(year,1,1)},
                                                            new Post() {Id = Guid.NewGuid(), PublishDate = new DateTime(year,1,1)},
                                                            new Post() {Id = Guid.NewGuid(), PublishDate = new DateTime(year,1,1)}
                                                        };

                                            posts_by_year = new PostsByYear() { January = posts.Where(p => p.PublishDate.Month == 1).Count(), Year = year};

                                            posts_by_year_entity_context.Populate(new[] {posts_by_year});
                                            posts_entity_context.Populate(posts);
                                        };
    }
}