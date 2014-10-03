using System;
using Web.Concepts.Persons;
using Bifrost.Read;

namespace Web.Read.HumanResources.Employees
{
    public class Employee : IReadModel
    {
        public Guid Id { get; set; }
        public SocialSecurityNumber SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EmployedFrom { get; set; }
    }
}
