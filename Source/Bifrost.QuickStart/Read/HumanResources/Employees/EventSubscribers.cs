using Bifrost.Events;
using Bifrost.QuickStart.Events.HumanResources.Employees;
using Bifrost.Read;

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
                Id = @event.EventSourceId,
                SocialSecurityNumber = @event.SocialSecurityNumber,
                FirstName = @event.FirstName,
                LastName = @event.LastName
            });
        }
    }
}
