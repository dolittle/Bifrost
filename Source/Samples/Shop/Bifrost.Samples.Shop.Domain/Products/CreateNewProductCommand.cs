using System;
using Bifrost.Commands;

namespace Bifrost.Samples.Shop.Domain.Products
{
	public class CreateNewProductCommand : ICommand
	{
		public Guid Id { get; set; }
	}
}
