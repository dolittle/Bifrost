using Bifrost.Commands;
using Bifrost.Domain;

namespace Bifrost.Samples.Blog.Domain.Posts.Commands
{
    public class PostCommandHandler : ICommandHandler
    {
        readonly IAggregatedRootFactory<Post> _factory;
        readonly IAggregatedRootRepository<Post> _repository;

        public PostCommandHandler(IAggregatedRootFactory<Post> factory, IAggregatedRootRepository<Post> repository)
        {
            _factory = factory;
            _repository = repository;
        }

        public void Handle(CreatePost createPost)
        {
            var post = _factory.Create(createPost.Id);
            post.Create(createPost.BlogId, createPost.Title);
            post.SetTitle(createPost.Title);
            post.SetBody(createPost.Body);
        }

        public void Handle(UpdatePost updatePost)
        {
            var post = _repository.Get(updatePost.Id);
            post.SetTitle(updatePost.Title);
            post.SetBody(updatePost.Body);
        }

		public void Handle(AddCommentToPost addCommentToPost)
		{
			var post = _repository.Get(addCommentToPost.Id);
			post.AddComment(
				addCommentToPost.Author, 
				addCommentToPost.EMail, 
				addCommentToPost.Url, 
				addCommentToPost.Comment, 
				addCommentToPost.Occured);
			
		}
    }
}