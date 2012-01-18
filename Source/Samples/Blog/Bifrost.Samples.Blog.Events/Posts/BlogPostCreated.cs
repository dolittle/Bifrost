using System;
using Bifrost.Events;
using Bifrost.Time;

namespace Bifrost.Samples.Blog.Events.Posts
{
    public class BlogPostCreated : PostCreated, IAmNextGenerationOf<PostCreated>
    {
        public BlogPostCreated(Guid postId, Guid blogId, string title)
            : base(postId,blogId,title)
        {
            BlogId = blogId;
            Title = title;
            PublishedDate = SystemClock.GetCurrentTime();
            CreatedAt = SystemClock.GetCurrentTime();
        }

        public BlogPostCreated(Guid postId) : base(postId)
        {}

        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}