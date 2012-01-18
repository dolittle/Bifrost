using System;
using Bifrost.Domain;

namespace Bifrost.Samples.Shop.Domain.Products
{
	public class Product : AggregatedRoot<Product>, IDynamicOriginator
	{
		public const string DefaultTitle = "New Product";

		private string _title;

		public Product()
		{

		}

		private Product(string title)
		{
			Apply(p => p.NewProductCreated(Guid.NewGuid()));
			Apply(p => p.TitleSet(title));
		}

		public dynamic CreateMemento()
		{
			return new
			{
				Title = _title
			};
		}

		public void SetMemento(dynamic memento)
		{
			_title = memento.Title;
		}

		public Product CreateNew()
		{
			var product = new Product(DefaultTitle);
			return product;
		}

		private void NewProductCreated(Guid id)
		{
			Id = id;
		}

		private void TitleSet(string title)
		{
			_title = title;
		}
	}
}
