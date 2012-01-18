using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Blogs
{
    public class BlogCreated : Event
    {
        public BlogCreated(Guid eventSourceId) : base(eventSourceId)
        {
            
        }
    }
}
