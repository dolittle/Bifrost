using System;
using System.Linq;
using Bifrost.Views;

namespace ImplicitConsole.Views
{
	public class PersonView : IPersonView
	{
		readonly IView<Person> _repository;

		public PersonView(IView<Person> repository)
		{
			_repository = repository;
		}

		public Person[] GetAll()
		{
			var persons = _repository.Query.ToArray();
			return persons;
		}

		public Person Get(Guid id)
		{
			var person = (from p in _repository.Query
						 where p.Id == id
			             select p).Single();
			return person;
		}
	}
}
