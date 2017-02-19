using System.Collections.Generic;
using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationStructure
{
    public class when_initializing
    {
        static IEnumerable<IStringFormat> structure_formats;
        static ApplicationStructure result;

        Establish context = () => structure_formats = new IStringFormat[0];

        Because of = () => result = new ApplicationStructure(structure_formats);

        It should_hold_structure_formats = () => result.StructureFormats.ShouldEqual(structure_formats);
    }
}
