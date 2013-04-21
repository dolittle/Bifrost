using Machine.Specifications;
using Bifrost.Commands;
using System;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_setting_type_and_name_is_specified : given.an_empty_command_builder
    {
        Establish context = () =>
        {
            builder.Name = "Known";
            conventions_mock.Setup(c => c.CommandName).Returns(n => "HelloCommand");
        };

        Because of = () => builder.Type = typeof(KnownCommand);

        It should_not_change_name = () => builder.Name.ShouldEqual("Known");
    }
}
