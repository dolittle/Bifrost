using Bifrost.Events;
using Bifrost.Samples.Blog.Events.Tags;

namespace Bifrost.Samples.Blog.Views.Tags
{
    public class TagEventSubscriber : EventSubscriber<Tag>
    {
        public void Process(TagCreated @event)
        {
            var tag = new Tag {Id = @event.EventSourceId };
            InsertEntity(tag);
        }

        public void Process(TagNameSet @event)
        {
            UpdateProperty(@event, t => t.Name = @event.TagName);
        }

        public void Process(TagDeleted @event)
        {
            DeleteEntity(@event);
        }
    }
}