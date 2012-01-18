using System;
using Bifrost.Domain;
using ExplicitConsole.Events;

namespace ExplicitConsole.Domain
{
	public class Person : AggregatedRoot
	{
		public Person(Guid id) : base(id)
		{
			Create();
		}
		
		public void Create()
		{
			Apply(new PersonCreated(Id));
		}

		public void SetFirstName(string firstName)
		{
			Apply(new FirstNameChanged(Id,firstName));
		}

		public void SetLastName(string lastName)
		{
			Apply(new LastNameChanged(Id,lastName));
		}


        public void Handle(PersonCreated created)
        {

        }

        public void Handle(FirstNameChanged firstNameChanged)
        {
        }

        
        public void Handle(LastNameChanged lastNameChanged)
		{
		}
	}
}