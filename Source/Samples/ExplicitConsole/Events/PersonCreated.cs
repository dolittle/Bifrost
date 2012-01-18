using System;
using Bifrost.Events;

namespace ExplicitConsole.Events
{
	public class PersonCreated : Event
	{
        public PersonCreated(Guid id) : base(id) {}
	}
}