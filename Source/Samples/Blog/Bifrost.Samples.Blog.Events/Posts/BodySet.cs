using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Posts
{
    public class BodySet : Event
    {
        public BodySet(Guid id, string body) : base(id)
        {
            Body = body;
        }

        public BodySet(Guid id) : base(id)
        {}

        public string Body { get; set; }
    }
}
