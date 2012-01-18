using System;
using System.Collections.Generic;

namespace Bifrost.Samples.Blog.Views.Posts
{
	public interface IPostView
	{
	    Post Get(Guid id);
		IEnumerable<Post> GetAllForBlog(Guid blogId);
	}
}