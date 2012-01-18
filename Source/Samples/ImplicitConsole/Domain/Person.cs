using System;
using Bifrost.Domain;

namespace ImplicitConsole.Domain
{
	public class Person : AggregatedRoot
	{
		public Person(Guid id) : base(id)
		{
			Apply(() => Create());
		}

		public void Create()
		{
		}

		public void SetFirstName(string firstName)
		{
			Apply(() => FirstNameChanged(firstName));
		}

		public void SetLastName(string lastName)
		{
			Apply(() => LastNameChanged(lastName));
		}

		private void FirstNameChanged(string firstName)
		{
		}

		private void LastNameChanged(string lastName)
		{
		}
	}
}
