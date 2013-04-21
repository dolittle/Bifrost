using System;
using Bifrost.Events;

namespace Bifrost.QuickStart.Events.HumanResources.Employees
{
    public class EmployeeRegistered : Event
    {
        public EmployeeRegistered(Guid eventSourceId) : base(eventSourceId) { }

        public string SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}