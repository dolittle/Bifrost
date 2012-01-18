using System;
using Bifrost.Events;

namespace Bifrost.Samples.Blog.Events.Blogs
{
    public class BlogTagLineSet : Event
    {
        public BlogTagLineSet(Guid blogId, string tagLine) : base(blogId)
        {
            TagLine = tagLine;
        }

        public string TagLine { get; set; }
    }
}
