using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_SecurityObjectExtensions.given
{
    public class a_fresh_security_object
    {
        protected static Mock<ISecurityObject> security_object_mock;
        protected static ISecurityObject security_object;

        Establish context = () =>
        {
            security_object_mock = new Mock<ISecurityObject>();
            security_object = security_object_mock.Object;
        };
    }
}
