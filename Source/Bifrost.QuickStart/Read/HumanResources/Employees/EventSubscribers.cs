using Bifrost.Events;
using Bifrost.Read;
using Bifrost.QuickStart.Events.HumanResources.Employees;

namespace Bifrost.QuickStart.Read.HumanResources.Employees
{
    public class EventSubscribers : IProcessEvents
    {
        IReadModelRepositoryFor<Employee> _repository;

        public EventSubscribers(IReadModelRepositoryFor<Employee> repository)
        {
            _repository = repository;
        }

        public void Process(EmployeeRegistered @event)
        {
            _repository.Insert(new Employee
            {
                SocialSecurityNumber = @event.SocialSecurityNumber,
                FirstName = @event.FirstName,
                LastName = @event.LastName
            });
        }
    }
}
