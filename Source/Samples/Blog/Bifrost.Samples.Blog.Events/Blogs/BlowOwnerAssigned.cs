using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Blogs
{
    public class BlogOwnerAssigned : Event
    {
        public BlogOwnerAssigned(Guid blogId, string owner) : base(blogId)
        {
            Owner = owner;
        }

        public string Owner { get; set; }
    }
}
