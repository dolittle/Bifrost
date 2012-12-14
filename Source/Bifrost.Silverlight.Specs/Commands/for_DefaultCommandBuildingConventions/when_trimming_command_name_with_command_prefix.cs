using Machine.Specifications;
using Bifrost.Commands;

namespace Bifrost.Silverlight.Specs.Commands.for_DefaultCommandBuildingConventions
{
    public class when_trimming_command_name_with_command_prefix
    {
        static string result;

        Because of = () => result = DefaultCommandBuildingConventions.TrimCommandFromName("CommandTest");

        It should_not_trim_anything = () => result.ShouldEqual("CommandTest");
    }
}
