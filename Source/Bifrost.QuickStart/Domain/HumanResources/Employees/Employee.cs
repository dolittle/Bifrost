using System;
using Bifrost.Domain;
using Web.Concepts.Persons;
using Web.Events.HumanResources.Employees;

namespace Web.Domain.HumanResources.Employees
{
    public class Employee : AggregateRoot
    {
        public Employee(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        public void Register(SocialSecurityNumber socialSecurityNumber, string firstName, string lastName, DateTime employedFrom)
        {
            Apply(new EmployeeRegistered(Id)
            {
                SocialSecurityNumber = socialSecurityNumber,
                FirstName = firstName,
                LastName = lastName,
                EmployedFrom = employedFrom
            });
        }

    }
}