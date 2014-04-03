using Bifrost.Security;
using Bifrost.Services;

namespace Bifrost.QuickStart.Domain.HumanResources.Foos
{
    public class SecurityDescriptor : BaseSecurityDescriptor
    {
        public SecurityDescriptor()
        {
            When.Invoking().Services().InstanceOf<SecuredService>(s => s.User().MustBeInRole("Create"));
        }
    }
}