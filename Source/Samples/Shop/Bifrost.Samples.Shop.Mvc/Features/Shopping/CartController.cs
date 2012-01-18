using System.Web.Mvc;
using Bifrost.Samples.Shop.Domain.Shopping;

namespace Bifrost.Samples.Shop.Mvc.Features.Shopping
{
    public class CartController : Controller
    {
    	private readonly CartHandlers _cartHandlers;

		public CartController(CartHandlers cartHandlers)
		{
			_cartHandlers = cartHandlers;
		}

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Add(AddProductToCart addProductToCart)
		{
			_cartHandlers.Handle(addProductToCart);
			return RedirectToAction("Index");
		}


    }
}
