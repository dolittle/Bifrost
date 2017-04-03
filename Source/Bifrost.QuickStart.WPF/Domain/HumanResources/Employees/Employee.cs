﻿using System;
using Bifrost.Domain;
using Bifrost.QuickStart.Concepts.Persons;
using Bifrost.QuickStart.Events.HumanResources.Employees;

namespace Bifrost.QuickStart.Domain.HumanResources.Employees
{
    public class Employee : AggregateRoot
    {
        public Employee(Guid eventSourceId)
            : base(eventSourceId)
        {
        }

        public void Register(SocialSecurityNumber socialSecurityNumber, string firstName, string lastName, DateTime employedFrom)
        {
            Apply(new EmployeeRegistered(EventSourceId)
            {
                SocialSecurityNumber = socialSecurityNumber,
                FirstName = firstName,
                LastName = lastName,
                EmployedFrom = employedFrom
            });
        }

    }
}