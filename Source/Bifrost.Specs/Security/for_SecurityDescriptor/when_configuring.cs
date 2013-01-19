using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    public class when_configuring
    {
        static SecurityDescriptor   descriptor;

        Because of = () => descriptor = new SecurityDescriptor();

        It should_have_a_when_clause_set = () => descriptor.When.ShouldNotBeNull();
    }
}
