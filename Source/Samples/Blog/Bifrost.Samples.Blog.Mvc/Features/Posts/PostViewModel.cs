using System.Collections.Generic;
using Bifrost.Samples.Blog.Views.Posts;

namespace Bifrost.Samples.Blog.Mvc.Features.Posts
{
	public class PostViewModel
	{
		public Post Post { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
	}
}