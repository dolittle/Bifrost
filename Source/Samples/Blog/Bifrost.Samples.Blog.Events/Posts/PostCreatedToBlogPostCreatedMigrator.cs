using Bifrost.Events;
using Bifrost.Time;

namespace Bifrost.Samples.Blog.Events.Posts
{
    public class PostCreatedToBlogPostCreatedMigrator : IEventMigrator<PostCreated, BlogPostCreated>
    {
        public BlogPostCreated Migrate(PostCreated source)
        {
            return new BlogPostCreated(source.Id, source.BlogId, source.Title)
                       {
                           PublishedDate = source.PublishedDate,
                           CreatedAt = SystemClock.GetCurrentTime()
                       };
        }
    }
}