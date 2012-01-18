using System;

namespace Bifrost.Samples.Shop.Query.Campaigns
{
	public class Campaign
	{
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
