using System;
using Bifrost.Commands;
using Bifrost.Domain;

namespace Bifrost.QuickStart.Domain.HumanResources.Employees
{
    public class CommandHandlers : IHandleCommands
    {
        IAggregateRootRepository<Employee> _repository;

        public CommandHandlers(IAggregateRootRepository<Employee> repository)
        {
            _repository = repository;
        }

        public void Handle(RegisterEmployee command)
        {
            var employee = _repository.Get(Guid.NewGuid());
            employee.Register(command.SocialSecurityNumber, command.FirstName, command.LastName);
        }
    }
}