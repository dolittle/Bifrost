using Bifrost.Commands;
using Bifrost.QuickStart.Concepts.Persons;

namespace Bifrost.QuickStart.Domain.HumanResources.Employees
{
    public class RegisterEmployee : Command
    {
        public SocialSecurityNumber SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class TestCommandWithMultiplePropertiesOfTheSameType : Command
    {
        public SocialSecurityNumber First { get; set; }
        public SocialSecurityNumber Second { get; set; }
    }
}