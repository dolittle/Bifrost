using System;
using Bifrost.QuickStart.Concepts.Persons;
using Bifrost.Read;

namespace Bifrost.QuickStart.Read.HumanResources.Employees
{
    public class Employee : IReadModel
    {
        public Guid Id { get; set; }
        public SocialSecurityNumber SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
