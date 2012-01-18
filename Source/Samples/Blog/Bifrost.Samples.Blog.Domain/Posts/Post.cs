using System;
using System.Diagnostics.Contracts;
using Bifrost.Domain;
using Bifrost.Samples.Blog.Events.Posts;

namespace Bifrost.Samples.Blog.Domain.Posts
{
    public class Post : AggregatedRoot
    {
        public Post(Guid id) : base(id)
        {
        }

        public void Create(Guid blogId, string title)
        {
            Contract.Requires(!blogId.Equals(Guid.Empty));
            Contract.Requires(!string.IsNullOrEmpty(title));

            Apply(new BlogPostCreated(Id, blogId, title));
        }

        public void SetTitle(string title)
        {
            Contract.Requires(!string.IsNullOrEmpty(title));

            Apply(new TitleSet(Id,title));
        }


        public void SetBody(string body)
        {
            Contract.Requires(!string.IsNullOrEmpty(body));

            Apply(new BodySet(Id,body));
        }

        public void AddTag(string tagName)
        {
            Contract.Requires(!string.IsNullOrEmpty(tagName));

            Apply(new TagAddedToPost(Id,tagName));
        }


		public void AddComment(string author, string email, string url, string comment, DateTime occured)
		{
			Contract.Requires(!string.IsNullOrEmpty(author));
			Contract.Requires(!string.IsNullOrEmpty(email));
			Contract.Requires(!string.IsNullOrEmpty(comment));

			Apply(new CommentAdded(Id, author, email, url, comment, occured));
		}
    }
}
