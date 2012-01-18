using System;
using System.Web.Mvc;
using Bifrost.Sagas;
using Bifrost.Samples.Blog.Sagas.Posts;

namespace Bifrost.Samples.Blog.Mvc.Areas.Administration.Features.Comments
{
	public class CommentsController : AdministrationController
	{
		readonly ISagaNarrator _sagaNarrator;
		readonly ISagaLibrarian _librarian;

		public CommentsController(ISagaNarrator sagaNarrator, ISagaLibrarian librarian)
		{
			_sagaNarrator = sagaNarrator;
			_librarian = librarian;
		}


		public ActionResult Index()
		{
			var sagas = _librarian.GetForPartition(SagaOfComments.PartitionName);
			return View(sagas);
		}

		public ActionResult Approve(Guid sagaId)
		{
			var saga = _librarian.Get(sagaId);
			_sagaNarrator.Conclude(saga);
			return RedirectToAction("Index");
		}
	}
}