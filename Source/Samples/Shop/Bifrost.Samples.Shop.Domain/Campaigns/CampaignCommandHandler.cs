using Bifrost.Commands;
using Bifrost.Domain;

namespace Bifrost.Samples.Shop.Domain.Campaigns
{
	public class CampaignCommandHandler : ICommandHandler
	{
		private readonly IAggregatedRootFactory<Campaign> _repository;

		public CampaignCommandHandler(IAggregatedRootFactory<Campaign> repository)
		{
			_repository = repository;
		}


		public void Handle(CreateNewCampaign createNewCampaign)
		{
			var campaign = _repository.Create(createNewCampaign.Id);
		}
	}
}