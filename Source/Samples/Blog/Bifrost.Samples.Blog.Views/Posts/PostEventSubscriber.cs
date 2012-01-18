using Bifrost.Events;
using Bifrost.Samples.Blog.Events.Posts;
using Bifrost.Samples.Blog.Events.Tags;

namespace Bifrost.Samples.Blog.Views.Posts
{
	public class PostEventSubscriber : EventSubscriber<Post>
	{
		public void Process(BlogPostCreated postCreated)
		{
			var post = new Post
			               {
			                   Id = postCreated.EventSourceId,
                               BlogId = postCreated.BlogId,
                               Title = postCreated.Title
			               };
			InsertEntity(post);
		}

		public void Process(TitleSet @event)
		{
            UpdateProperty(@event, p=>p.Title = @event.Title);
		}

        public void Process(BodySet @event)
        {
            UpdateProperty(@event, p => p.Body = @event.Body);
        }

        public void Process(PublishDateSet @event)
        {
            UpdateProperty(@event, p => p.PublishDate = @event.PublishDate);
        }

        public void Process(TagAddedToPost @event)
        {
            
        }

        public void Process(TagDeleted @event)
        {

        }
	}
}
