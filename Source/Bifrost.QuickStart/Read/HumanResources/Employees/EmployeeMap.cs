//using Bifrost.DocumentDB.Mapping;
using Bifrost.Entities.Files.Mapping;

namespace Web.Read.HumanResources.Employees
{
    public class EmployeeMap : DocumentMapFor<Employee>
    {
        public EmployeeMap()
        {
            Property(e => e.Id).Key();
        }
    }
}