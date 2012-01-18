using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Posts
{
    public class TitleSet : Event
    {
        public TitleSet(Guid id, string title) : base(id)
        {
            Title = title;
        }

        public TitleSet(Guid id) : base(id)
        {}

        public string Title { get; set; }
    }
}