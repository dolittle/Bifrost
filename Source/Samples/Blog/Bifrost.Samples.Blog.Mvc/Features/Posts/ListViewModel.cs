using System;
using System.Collections.Generic;
using Bifrost.Samples.Blog.Views.Posts;

namespace Bifrost.Samples.Blog.Mvc.Features.Posts
{
    public class ListViewModel
    {
        public ListViewModel(Guid blogId, IEnumerable<Post> posts)
        {
            BlogId = blogId;
            Posts = posts;
        }

        public Guid BlogId { get; private set; }
        public IEnumerable<Post> Posts { get; private set; }
    }
}