using Bifrost.Security;

namespace Bifrost.Specs.Security.for_SecurityObject
{
    public class SecurityRule : ISecurityRule
    {
        public bool HasAccessCalled = false;
        public object SecurablePassedToHasAccess;
        public bool HasAccessResult = true;
        public bool HasAccess(object securable)
        {
            SecurablePassedToHasAccess = securable;
            HasAccessCalled = true;
            return HasAccessResult;
        }
    }
}
