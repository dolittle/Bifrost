using System;

namespace Bifrost.Samples.Shop.Mvc.Features.Products
{
	public class Product
	{
		public Guid Id { get; set; }
		public string MaterialNumber { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
	}
}