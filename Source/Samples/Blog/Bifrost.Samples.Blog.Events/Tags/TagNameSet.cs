using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Tags
{
    public class TagNameSet : Event
    {
        public TagNameSet(Guid tagId, string tagName) : base(tagId)
        {
            TagName = tagName;
        }

        public string TagName { get; set; }
    }
}
