using System;
using Bifrost.Events;
using Bifrost.Samples.Blog.Events.Posts;

namespace Bifrost.Samples.Blog.Views.Posts
{
	public class CommentEventSubscriber : EventSubscriber<Comment>
	{
		public void Process(CommentAdded @event)
		{
			var comment = new Comment
			              	{
			              		Id = Guid.NewGuid(),
			              		Author = @event.Author,
			              		EMail = @event.EMail,
			              		Url = @event.Url,
			              		Occured = @event.Occured,
			              		PostId = @event.EventSourceId,
			              		Body = @event.Comment
			              	};
			InsertEntity(comment);
		}
	}
}