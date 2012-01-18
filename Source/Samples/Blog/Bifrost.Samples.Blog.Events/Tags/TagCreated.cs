using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Tags
{
    public class TagCreated : Event   
    {
        public TagCreated(Guid tagId) : base(tagId) {}
    }
}
