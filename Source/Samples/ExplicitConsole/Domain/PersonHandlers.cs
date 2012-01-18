using Bifrost.Commands;
using Bifrost.Domain;

namespace ExplicitConsole.Domain
{
	public class PersonHandlers : ICommandHandler
	{
        private readonly IAggregatedRootFactory<Person> _factory;
		
		public PersonHandlers(IAggregatedRootFactory<Person> factory)
		{
            _factory = factory;
		}
		
		public void Handle(CreatePerson command)
		{
            var aggregate = _factory.Create(command.Id);
			aggregate.SetFirstName(command.FirstName);
			aggregate.SetLastName(command.LastName);
		}
	}
}