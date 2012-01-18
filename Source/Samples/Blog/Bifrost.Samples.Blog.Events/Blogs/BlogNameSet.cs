using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Blogs
{
    public class BlogNameSet : Event
    {
        public BlogNameSet(Guid blogId, string blogName) : base(blogId)
        {
            BlogName = blogName;
        }

        public string BlogName { get; set; }
    }
}
