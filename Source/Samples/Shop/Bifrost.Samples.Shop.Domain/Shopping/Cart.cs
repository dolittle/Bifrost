using System;
using Bifrost.Domain;
using Bifrost.Samples.Shop.Domain.Products;

namespace Bifrost.Samples.Shop.Domain.Shopping
{
	public class Cart : AggregatedRoot<Cart>
	{
		private Cart(Guid id)
		{
			Apply(c => c.CartCreated(id));
		}

		public static Cart Create(Guid id)
		{
			var cart = new Cart(id);
			return cart;
		}


		public void AddProduct(Product product)
		{
			Apply(c => c.ProductAdded(product));
		}


		private void CartCreated(Guid id)
		{
			
		}

		private void ProductAdded(Product product)
		{
			
		}
	}
}