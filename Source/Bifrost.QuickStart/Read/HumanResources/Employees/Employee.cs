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
        public DateTime DateProp { get; set; }
        public DateTime? DateNullProp { get; set; }
        public int? IntNullProp { get; set; }
        public int IntProp { get; set; }
        public System.Collections.Generic.IEnumerable<string> EnumerableProp { get; set; }
        public bool BooleanProp { get; set; }

    }
}
