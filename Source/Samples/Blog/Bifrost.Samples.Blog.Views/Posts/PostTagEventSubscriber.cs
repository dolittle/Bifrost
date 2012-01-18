using Bifrost.Events;
using Bifrost.Samples.Blog.Events.Tags;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostTagEventSubscriber : EventSubscriber<PostTag>
    {
        public void Process(TagCreated @event)
        {
            var postTag = new PostTag {Id = @event.EventSourceId};
            InsertEntity(postTag);
        }

        public void Process(TagNameSet @event)
        {
            UpdateProperty(@event, p => p.Name = @event.TagName);
        }

        public void Process(TagDeleted @event)
        {
            DeleteEntity(@event);
        }
    }
}
