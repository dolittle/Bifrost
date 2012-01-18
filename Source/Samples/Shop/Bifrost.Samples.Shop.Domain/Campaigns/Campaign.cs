using System;
using Bifrost.Domain;

namespace Bifrost.Samples.Shop.Domain.Campaigns
{
	public class Campaign : AggregatedRoot
	{
		private CampaignStatus _status;


		public Campaign(Guid id) : base(id)
		{
			Apply(() => Created(id));
		}


		public void Pause()
		{
			Apply(() => Paused());
		}


		private void Created(Guid id)
		{
			Id = id;
		}

		private void Paused()
		{
			_status = CampaignStatus.Paused;
		}
	}
}
