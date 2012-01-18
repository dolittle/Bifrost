using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Posts
{
    public class TagAddedToPost : Event
    {
        public TagAddedToPost(Guid id, string tagName) : base(id)
        {
            TagName = tagName;
        }

        public string TagName { get; set; }
    }
}
