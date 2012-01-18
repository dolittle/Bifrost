using System;
using Bifrost.Events;

namespace ExplicitConsole.Events
{
	public class LastNameChanged : Event
	{
		public LastNameChanged(Guid id, string lastName) : base(id)
		{
			LastName = lastName;
		}

		public string LastName { get; set; }
	}
}