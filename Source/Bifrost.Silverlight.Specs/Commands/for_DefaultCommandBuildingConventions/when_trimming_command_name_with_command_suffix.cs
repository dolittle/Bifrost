using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_DefaultCommandBuildingConventions
{
    public class when_trimming_command_name_with_command_suffix
    {
        static string result;

        Because of = () => result = DefaultCommandBuildingConventions.TrimCommandFromName("TestCommand");

        It should_have_only_actual_name_left = () => result.ShouldEqual("Test");
    }
}
