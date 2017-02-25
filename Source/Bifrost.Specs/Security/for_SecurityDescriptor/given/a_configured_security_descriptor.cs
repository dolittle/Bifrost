using Bifrost.Configuration;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Bifrost.Security;
using Bifrost.SomeRandomNamespace;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_SecurityDescriptor.given
{
    public class a_configured_security_descriptor
    {
        protected static SecurityDescriptor security_descriptor;
        protected static SimpleCommand command_that_has_namespace_and_type_rule;
        protected static AnotherSimpleCommand command_that_has_namespace_rule;
        protected static CommandInADifferentNamespace command_that_is_not_applicable;
        protected static Mock<ICanResolvePrincipal> resolve_principal_mock;

        Establish context = () =>
            {
                resolve_principal_mock = new Mock<ICanResolvePrincipal>();
                var currentConfigure = Configure.With(Mock.Of<IContainer>(), (IDefaultConventions) null, null, null);
                Mock.Get(currentConfigure.Container)
                    .Setup(m => m.Get<ICanResolvePrincipal>())
                    .Returns(resolve_principal_mock.Object);

                security_descriptor = new SecurityDescriptor();
                command_that_has_namespace_and_type_rule = new SimpleCommand();
                command_that_has_namespace_rule = new AnotherSimpleCommand();
                command_that_is_not_applicable = new CommandInADifferentNamespace();
            };
    }
}