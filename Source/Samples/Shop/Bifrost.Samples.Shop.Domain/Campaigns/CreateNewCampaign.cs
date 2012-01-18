using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Shop.Domain.Campaigns
{
	public class CreateNewCampaign : ICommand
	{
		public CreateNewCampaign()
		{
			Id = Guid.NewGuid();
		}


		public Guid Id { get; private set; }

		public string Name { get; set; }
		public string Title { get; set; }
		public CampaignStatus Status { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidTo { get; set; }
		public CampaignZone Zone { get; set; }
		public string Sites { get; set; }
		public string Tag { get; set; }
		public Segment Segment { get; set; }


	}
}