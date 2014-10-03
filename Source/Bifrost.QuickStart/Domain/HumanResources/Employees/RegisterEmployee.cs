using System;
using Bifrost.Commands;
using Web.Concepts.Persons;

namespace Web.Domain.HumanResources.Employees
{
    public class RegisterEmployee : Command
    {
        public SocialSecurityNumber SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EmployedFrom { get; set; }
    }

    public class TestCommandWithMultiplePropertiesOfTheSameType : Command
    {
        public SocialSecurityNumber First { get; set; }
        public SocialSecurityNumber Second { get; set; }
    }
}