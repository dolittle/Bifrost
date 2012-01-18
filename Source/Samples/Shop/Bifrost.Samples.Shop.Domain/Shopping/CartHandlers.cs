using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Samples.Shop.Domain.Products;

namespace Bifrost.Samples.Shop.Domain.Shopping
{
	public class CartHandlers : ICommandHandler
	{
		private readonly IAggregatedRootRepository<Cart> _cartRootRepository;
		private readonly IAggregatedRootRepository<Product> _productRootRepository;

		public CartHandlers(
			IAggregatedRootRepository<Cart> cartRootRepository, 
			IAggregatedRootRepository<Product> productRootRepository)
		{
			_cartRootRepository = cartRootRepository;
			_productRootRepository = productRootRepository;
		}


		public void Handle(AddProductToCart productToCart)
		{
			var cart = _cartRootRepository.Find(productToCart.Id);
			if( null == cart )
			{
				cart = Cart.Create(productToCart.Id);
			}
			var product = _productRootRepository.Get(productToCart.ProductId);
			cart.AddProduct(product);
		}
	}
}