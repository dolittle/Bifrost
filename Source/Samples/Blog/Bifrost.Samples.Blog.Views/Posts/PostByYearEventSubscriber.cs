using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Samples.Blog.Events.Posts;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostStatisticsSubscriber : IEventSubscriber
    {
        readonly IPostStatisticsService _statisticsService;


        public PostStatisticsSubscriber(IPostStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }


        public void Process(PostCreated postCreated)
        {
            _statisticsService.AggregateForMonth(postCreated.PublishedDate.Year,postCreated.PublishedDate.Month,new[] {postCreated.EventSourceId});
        }
    }
}
