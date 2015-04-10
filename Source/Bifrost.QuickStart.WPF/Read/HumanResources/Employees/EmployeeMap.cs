//using Bifrost.DocumentDB.Mapping;
using Bifrost.Entities.Files.Mapping;

namespace Bifrost.QuickStart.Read.HumanResources.Employees
{
    public class EmployeeMap : DocumentMapFor<Employee>
    {
        public EmployeeMap()
        {
            Property(e => e.Id).Key();
        }
    }
}