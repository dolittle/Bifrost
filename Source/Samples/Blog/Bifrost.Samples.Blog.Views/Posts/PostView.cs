using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Views;

namespace Bifrost.Samples.Blog.Views.Posts
{
	public class PostView : IPostView
	{
		readonly IView<Post> _repository;

		public PostView(IView<Post> repository)
		{
			_repository = repository;
		}

	    public Post Get(Guid id)
	    {
	        var post = (from p in _repository.Query
	                    where p.Id == id
	                    select p).Single();
	        return post;
	    }

	    public IEnumerable<Post> GetAllForBlog(Guid blogId)
	    {
	        var posts = from p in _repository.Query
                        where p.BlogId == blogId
	                    select p;
			return posts;
		}
	}
}
