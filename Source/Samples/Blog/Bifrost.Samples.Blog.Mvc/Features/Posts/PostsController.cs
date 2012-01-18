using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bifrost.Commands;
using Bifrost.Sagas;
using Bifrost.Samples.Blog.Sagas.Posts;
using Bifrost.Views;
using Bifrost.Samples.Blog.Domain.Posts;
using Bifrost.Samples.Blog.Domain.Posts.Commands;
using Bifrost.Samples.Blog.Views.Posts;
using Post = Bifrost.Samples.Blog.Views.Posts.Post;

namespace Bifrost.Samples.Blog.Mvc.Features.Posts
{
	public class PostsController : Controller
	{
		readonly IPostView _posts;
		readonly IView<Comment> _commentsView;
		readonly ICommandCoordinator _commandCoordinator;
		readonly ISagaNarrator _sagaNarrator;

		public PostsController(IPostView posts, IView<Comment> commentsView, ICommandCoordinator commandCoordinator, ISagaNarrator sagaNarrator)
		{
			_posts = posts;
			_commentsView = commentsView;
			_commandCoordinator = commandCoordinator;
			_sagaNarrator = sagaNarrator;
		}

		public ActionResult List(Guid blogId)
		{
			var posts = _posts.GetAllForBlog(blogId);
			var viewModel = new ListViewModel(blogId, posts);
			return View("List", viewModel);
		}

		public ActionResult Show(Guid id)
		{
			var post = _posts.Get(id);
			var viewModel = GetPostViewModelFromPost(post);
			return View("Show", viewModel);
		}

		public ActionResult Create(CreatePost createPost)
		{
			if (ModelState.IsValid)
			{
				var result = _commandCoordinator.Handle(createPost);

				if (!result.Success)
				{
					if (result.Invalid)
					{
						foreach (var validation in result.ValidationResults)
						{
							ModelState.AddModelError(validation.MemberNames.FirstOrDefault() ?? string.Empty, validation.ErrorMessage);
						}
					}
					else
					{
						return RedirectToAction("List", new { blogId = createPost.BlogId });
					}
				}
				else
				{
					var post = _posts.Get(createPost.Id);
					var viewModel = GetPostViewModelFromPost(post);
					return View("Show", viewModel);
				}
			}
			return
				RedirectToAction("List", new { blogId = createPost.BlogId });
		}

		[HttpGet]
		public ActionResult Edit(Guid id)
		{
			var post = _posts.Get(id);
			return View("Edit", post);
		}

		[HttpPost]
		public ActionResult Update(UpdatePost updatePost)
		{
			var post = _posts.Get(updatePost.Id);
			if (ModelState.IsValid)
			{
				var result = _commandCoordinator.Handle<Domain.Posts.Post>(updatePost.Id, p => p.SetBody(updatePost.Body));
				//var result = _commandCoordinator.Handle(updatePost);

				if (result.Exception != null)
					return RedirectToAction("List");


				return View("Show", GetPostViewModelFromPost(post));
			}
			return
				RedirectToAction("Show", GetPostViewModelFromPost(post));
		}

		[HttpPost]
		public ActionResult AddComment(AddCommentToPost addComment)
		{
			var post = _posts.Get(addComment.Id);
			if (ModelState.IsValid)
			{
				var saga = GetOrCreateSagaOfComments(post);
				_sagaNarrator.TransitionTo<Comments>(saga);
				addComment.Occured = DateTime.Now;
				_commandCoordinator.Handle(saga, addComment);
			}
			return View("Show", GetPostViewModelFromPost(post));
		}

		private ISaga GetOrCreateSagaOfComments(Post post)
		{
			var key = string.Format("{0}_Comments", post.Id);
			if (HttpContext.Session[key] == null)
			{
				var saga = _sagaNarrator.Begin<SagaOfComments>();
				HttpContext.Session[key] = saga.Id;
				return saga;
			}
			else
			{
				var sagaId = (Guid)HttpContext.Session[key];
				return _sagaNarrator.Continue(sagaId);
			}
		}


		private PostViewModel GetPostViewModelFromPost(Post post)
		{
			var viewModel = new PostViewModel
			                	{
			                		Post = post,
			                		Comments = from c in _commentsView.Query
			                		           where c.PostId == post.Id
			                		           select c
			                	};
			return viewModel;
		}
	}
}
