﻿using Bifrost.Commands;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Commands.for_CommandCoordinator
{
    [Subject(typeof(CommandCoordinator))]
    public class when_handling_a_command_that_fails_security : given.a_command_coordinator
    {
        static CommandResult result;
        static Mock<AuthorizationResult> authorization_result;

        Establish context = () => 
        {
            authorization_result = new Mock<AuthorizationResult>();
            authorization_result.Setup(r => r.IsAuthorized).Returns(false);
            authorization_result.Setup(r => r.BuildFailedAuthorizationMessages()).Returns(new[] { "Something went wrong" });
            command_security_manager_mock.Setup(c => c.Authorize(command)).Returns(authorization_result.Object);
        };

        Because of = () => result = coordinator.Handle(command);

        It should_not_validate = () => command_validators_mock.Verify(c => c.Validate(command), Moq.Times.Never());
        It should_set_not_passed_in_command_result = () => result.PassedSecurity.ShouldBeFalse();
        It should_rollback_the_command_context = () => command_context_mock.Verify(c => c.Rollback(), Moq.Times.Once());
    }
}
