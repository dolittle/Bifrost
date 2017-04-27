using System;
using Bifrost.Commands;
using Bifrost.Domain;

namespace Web.Domain.HumanResources.Employees
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
            //var id = Guid.Parse("6077b565-36a1-4d99-b848-b65159282fa6");
            var id = Guid.NewGuid();
            var employee = _repository.Get(id);
            employee.Register(
                command.SocialSecurityNumber, 
                command.FirstName, 
                command.LastName,
                command.EmployedFrom
            );
        }
    }
}