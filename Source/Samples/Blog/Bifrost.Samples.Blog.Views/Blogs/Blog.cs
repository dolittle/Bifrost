using System;
using Bifrost.Views;

namespace Bifrost.Samples.Blog.Views.Blogs
{
    public class Blog : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Owner { get; set; }
    }
}
