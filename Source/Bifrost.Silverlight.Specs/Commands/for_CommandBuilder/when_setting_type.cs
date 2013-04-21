using Machine.Specifications;
using Bifrost.Commands;
using System;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class when_setting_type : given.an_empty_command_builder
    {
        Establish context = () => conventions_mock.Setup(c => c.CommandName).Returns(n => "HelloCommand");

        Because of = () => builder.Type = typeof(KnownCommand);

        It should_set_name_according_to_convention = () => builder.Name.ShouldEqual("HelloCommand");
    }
}
