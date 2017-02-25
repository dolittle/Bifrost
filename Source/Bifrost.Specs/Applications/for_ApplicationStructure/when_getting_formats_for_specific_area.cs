using System.Collections.Generic;
using Bifrost.Strings;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationStructure
{
    public class when_getting_formats_for_specific_area : given.two_areas_with_one_format_in_each
    {
        static IEnumerable<IStringFormat> result;

        Because of = () => result = structure.GetStructureFormatsForArea(second_area);

        It should_hold_the_second_format = () => result.ShouldContainOnly(second_format.Object);
    }
}
