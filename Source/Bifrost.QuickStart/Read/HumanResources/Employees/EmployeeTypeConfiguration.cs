using System.Data.Entity.ModelConfiguration;

namespace Web.Read.HumanResources.Employees.EntityFramework
{
    public class EmployeeTypeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeTypeConfiguration()
        {
            //Property(e=>e.SocialSecurityNumber)
        }
    }
}
