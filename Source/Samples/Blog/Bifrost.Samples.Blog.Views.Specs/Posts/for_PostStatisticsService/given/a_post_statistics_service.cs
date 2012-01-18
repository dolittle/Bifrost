using Bifrost.Samples.Blog.Views.Posts;
using Machine.Specifications;
using Bifrost.Fakes.Entities;

namespace Bifrost.Samples.Blog.Views.Specs.Posts.for_PostStatisticsService.given
{
    public class a_post_statistics_service
    {
        protected static EntityContext<PostsByYear> posts_by_year_entity_context;
        protected static EntityContext<Post> posts_entity_context;
        protected static IPostStatisticsService post_statistics_service;
		
		Establish context = () =>
								{
									posts_by_year_entity_context = new EntityContext<PostsByYear>();
									posts_entity_context = new EntityContext<Post>();
									post_statistics_service = new PostStatisticsService(posts_entity_context,
																						posts_by_year_entity_context);
								};
    }
}