using System.Linq;
using System.Web.Mvc;
using Bifrost.Commands;
using Bifrost.Views;
using Bifrost.Samples.Blog.Domain.Tags.Commands;
using Bifrost.Samples.Blog.Views.Tags;

namespace Bifrost.Samples.Blog.Mvc.Features.Tags
{
    public class TagsController : Controller
    {
        readonly IView<Tag> _repository;
        readonly ICommandCoordinator _commandCoordinator;

        public TagsController(IView<Tag> repository, ICommandCoordinator commandCoordinator)
        {
            _repository = repository;
            _commandCoordinator = commandCoordinator;
        }

        public ActionResult List()
        {
            var tags = _repository.Query.ToArray();
            return View(tags);
        }


        public ActionResult Delete(DeleteTag deleteTag)
        {
            _commandCoordinator.Handle(deleteTag);
            return RedirectToAction("List");
        }

        public ActionResult Create(CreateTag createTag)
        {
            _commandCoordinator.Handle(createTag);
            return RedirectToAction("List");
        }

    }
}
