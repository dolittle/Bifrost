using System.Collections.Generic;
using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationStructure
{
    public class when_initializing_with_two_structure_formats_with_different_areas : given.two_areas_with_one_format_in_each
    {
        static ApplicationStructure result;

        Because of = () => result = new ApplicationStructure(new Dictionary<ApplicationArea, IEnumerable<IStringFormat>> {
            { first_area, new IStringFormat[] { first_format.Object } },
            { second_area, new IStringFormat[] { second_format.Object } },
        });

        It should_hold_structure_formats = () => result.AllStructureFormats.ShouldContainOnly(first_format.Object, second_format.Object);
    }
}
