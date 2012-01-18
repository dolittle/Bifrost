using System;
using Bifrost.Commands;

namespace ExplicitConsole.Domain
{
	public class CreatePerson : ICommand
	{
		public Guid Id { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		
		public CreatePerson(Guid id, string firstName, string lastName)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
		}
	}
}