using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Shop.Domain.Shopping
{
	public class AddProductToCart : ICommand
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
	}
}
