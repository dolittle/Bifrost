using Bifrost.Events;
using Bifrost.Samples.Blog.Events.Blogs;

namespace Bifrost.Samples.Blog.Views.Blogs
{
    public class BlogEventSubscriber : EventSubscriber<Blog>
    {
        public void Process(BlogCreated @event)
        {
            var blog = new Blog {Id = @event.EventSourceId};
            InsertEntity(blog);
        }

        public void Process(BlogNameSet @event)
        {
            UpdateProperty(@event, b => b.Name = @event.BlogName);
        }

        public void Process(BlogTagLineSet @event)
        {
            UpdateProperty(@event, b => b.TagLine = @event.TagLine);
        }

        public void Process(BlogOwnerAssigned @event)
        {
            UpdateProperty(@event, b => b.Owner = @event.Owner);
        }
    }
}
