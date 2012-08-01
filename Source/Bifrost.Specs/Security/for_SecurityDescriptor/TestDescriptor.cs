using Bifrost.Security;
using Bifrost.Commands;

namespace Bifrost.Specs.Security.for_SecurityDescriptor
{
    public class MyCommand : Command
    {
    }

    public class TestDescriptor : SecurityDescriptor
    {
        public TestDescriptor()
        {
            ForNamespaceOf<MyCommand>()
                .User.MustBeInRole("User");

            For<MyCommand>()
                .When()
                    .TenantIs(null)
                    .ExecutionContextValue(e=>e.System == "Blah")
                    .User.MustBeInRole("Administrator");
        }
    }
}
