using Bifrost.Security;

namespace Bifrost.QuickStart.Domain.HumanResources.Employees
{
    public class SecurityDescriptor : BaseSecurityDescriptor
    {
        public SecurityDescriptor()
        {
            //When.Handling().Commands().InstanceOf<RegisterEmployee>(s => s.User().MustBeInRole("Create"));
        }
    }
}