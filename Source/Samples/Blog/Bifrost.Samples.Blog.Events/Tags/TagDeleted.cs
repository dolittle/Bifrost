using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Tags
{
    public class TagDeleted : Event
    {
        public TagDeleted(Guid tagId) : base(tagId) {}
    }
}