using System;
using Bifrost.Events;

namespace ExplicitConsole.Events
{
	public class FirstNameChanged : Event
	{
		public FirstNameChanged(Guid id, string firstName) : base(id)
		{
			FirstName = firstName;
		}

		public string FirstName { get; set; }
	}
}