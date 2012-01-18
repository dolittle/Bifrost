using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bifrost.Commands;
using Bifrost.Query;
using Bifrost.Samples.Shop.Domain.Campaigns;

namespace Bifrost.Samples.Shop.Mvc.Features.Campaigns
{
	public class CampaignsController : Controller
	{
		private readonly IQueryRepository<Query.Campaigns.Campaign> _repository;
		private readonly ICommandCoordinator _coordinator;

		public CampaignsController(
			IQueryRepository<Query.Campaigns.Campaign> repository,
			ICommandCoordinator coordinator)
		{
			_repository = repository;
			_coordinator = coordinator;
		}


		public ActionResult New()
		{
			return View("New");
		}

		public ActionResult Create(CreateNewCampaign createNewCampaign)
		{
			_coordinator.Handle(createNewCampaign);
			return Index();
		}

		public ActionResult Index()
		{
			var query = from c in _repository.Query
						select c;

			var campaigns = new List<Campaign>();
			foreach( var campaign in query )
			{
				campaigns.Add(new Campaign
				              	{
				              		Name = campaign.Name,
				              		Status = campaign.Status.ToString(),
				              		Title = campaign.Title,
									Zone = campaign.Zone.Name

				              	});

			}

			return View("List", campaigns);
		}

	}
}
