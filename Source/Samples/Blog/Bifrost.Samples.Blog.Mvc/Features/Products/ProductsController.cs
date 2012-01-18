using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bifrost.Samples.Shop.Mvc.Features.Products
{
    public class ProductsController : Controller
    {
        //
        // GET: /Posts/

        public ActionResult Index()
        {
        	var products = new List<Product>();
			for(int i = 1; i <= 10; i++)
			{
				var product = new Product
				              	{
									Id = Guid.NewGuid(),
									MaterialNumber =  i.ToString(),
				              		Title = "Product #" + i,
				              		Description = "Something"
				              	};
				products.Add(product);
			}

            return View(products);
        }

    }
}
