using System;
using System.Linq;
using Bifrost.Entities;
using Bifrost.Events;
using ExplicitConsole.Events;

namespace ExplicitConsole.Views
{
	public class PersonEventSubscriber : IEventSubscriber
	{
		readonly IEntityContext<Person> _entityContext;

		public PersonEventSubscriber(IEntityContext<Person> entityContext)
		{
			_entityContext = entityContext;
		}

		public void Process(PersonCreated personCreated)
		{
			var person = new Person();
			person.Id = personCreated.EventSourceId;
			_entityContext.Insert(person);
		    _entityContext.Commit();
		}

		public void Process(FirstNameChanged firstNameChanged)
		{
			var person = GetPerson(firstNameChanged.EventSourceId);
			person.FirstName = firstNameChanged.FirstName;
			_entityContext.Update(person);
			_entityContext.Commit();
		}

		public void Process(LastNameChanged lastNameChanged)
		{
			var person = GetPerson(lastNameChanged.EventSourceId);
			person.LastName = lastNameChanged.LastName;
			_entityContext.Update(person);
			_entityContext.Commit();
		}

		private Person GetPerson(Guid id)
		{
			var person = (from p in _entityContext.Entities
						  where p.Id == id
						  select p).Single();
			return person;
		}
	}
}