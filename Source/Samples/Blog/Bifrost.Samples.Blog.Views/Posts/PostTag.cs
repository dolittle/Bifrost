using System;
using Bifrost.Views;

namespace Bifrost.Samples.Blog.Views.Posts
{
    public class PostTag : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}