using System;
using Bifrost.Events;
using Bifrost.Time;

namespace Bifrost.Samples.Blog.Events.Posts
{
	public class PostCreated : Event
	{
        public PostCreated(Guid postId, Guid blogId, string title) : base(postId)
        {
            BlogId = blogId;
            Title = title;
            PublishedDate = SystemClock.GetCurrentTime();
        }

        public PostCreated(Guid postId) : base(postId)
        {}

        public Guid BlogId { get; set; }
        public string Title { get; set; }
	    public DateTime PublishedDate { get; set; }
	}
}
