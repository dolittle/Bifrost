using System.Linq;
using System.Web.Mvc;
using Bifrost.Commands;
using Bifrost.Views;
using Bifrost.Samples.Blog.Domain.Blogs.Commands;
using BlogEntity = Bifrost.Samples.Blog.Views.Blogs;

namespace Bifrost.Samples.Blog.Mvc.Features.Blogs
{
    public class BlogsController : Controller
    {
        readonly IView<BlogEntity.Blog> _repository;
        readonly ICommandCoordinator _commandCoordinator;

        public BlogsController(IView<BlogEntity.Blog> repository, ICommandCoordinator commandCoordinator)
        {
            _repository = repository;
            _commandCoordinator = commandCoordinator;
        }


        public ActionResult List()
        {
            var blogs = _repository.Query.ToArray();
            return View(blogs);
        }

        //[Authorize]
        public ActionResult Create(CreateBlog createBlog)
        {
            _commandCoordinator.Handle(createBlog);
            return RedirectToAction("List", "Posts", new { blogId =  createBlog.Id});
        }
    }
}
