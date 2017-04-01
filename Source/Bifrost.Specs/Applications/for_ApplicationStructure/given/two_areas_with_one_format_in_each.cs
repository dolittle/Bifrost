using System.Collections.Generic;
using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Applications.for_ApplicationStructure.given
{
    public class two_areas_with_one_format_in_each
    {
        protected static ApplicationArea first_area = "FirstArea";
        protected static ApplicationArea second_area = "SecondArea";
        protected static Mock<IStringFormat> first_format;
        protected static Mock<IStringFormat> second_format;
        protected static Dictionary<ApplicationArea, IEnumerable<IStringFormat>> string_formats_by_identifiers;
        protected static ApplicationStructure structure;

        Establish context = () =>
        {
            first_format = new Mock<IStringFormat>();
            second_format = new Mock<IStringFormat>();

            string_formats_by_identifiers = new Dictionary<ApplicationArea, IEnumerable<IStringFormat>> {
                { first_area, new IStringFormat[] { first_format.Object } },
                { second_area, new IStringFormat[] { second_format.Object } },
            };
            structure = new ApplicationStructure(string_formats_by_identifiers);
        };


    }
}
