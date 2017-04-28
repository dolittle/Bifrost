﻿using Bifrost.Events;
using Web.Events.HumanResources.Employees;
using Bifrost.Read;

namespace Web.Read.HumanResources.Employees
{
    public class EventProcessors : IProcessEvents
    {
        IReadModelRepositoryFor<Employee> _repository;

        public EventProcessors(IReadModelRepositoryFor<Employee> repository)
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
                LastName = @event.LastName,
                EmployedFrom = @event.EmployedFrom
            });
        }
    }
}
