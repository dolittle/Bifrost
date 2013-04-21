using System;
using Bifrost.Commands;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_getting_an_instance_of_command_with_ambiguous_constructors
    {
        static CommandBuilder<KnownCommandWithAmbiguousConstructors> builder;
        static Exception exception;

        Establish context = () =>
        {
            var commandCoordinatorMock = new Mock<ICommandCoordinator>();
            var commandBuildingConventionsMock = new Mock<ICommandBuildingConventions>();
            commandBuildingConventionsMock.Setup(c=>c.CommandName).Returns(n=>n);
            builder = new CommandBuilder<KnownCommandWithAmbiguousConstructors>(commandCoordinatorMock.Object, commandBuildingConventionsMock.Object);
        };

        Because of = () => exception = Catch.Exception(() => builder.GetInstance());

        It should_throw_ambiguous_constructors_exception = () => exception.ShouldBeOfType<AmbiguousConstructorsException>();
    }
}
