using Bifrost.Commands;
using Bifrost.Domain;

namespace ImplicitConsole.Domain
{
	public class CreatePerson : AggregatedRootCommand<Person>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
