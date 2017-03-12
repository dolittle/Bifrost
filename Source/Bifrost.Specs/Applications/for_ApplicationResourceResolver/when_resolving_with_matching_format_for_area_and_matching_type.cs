using System;
using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResourceResolver
{
    public class when_resolving_with_matching_format_for_area_and_matching_type : given.no_resolvers
    {
        class Implementation : IInterface { }

        static Type result;

        Establish context = () =>
        {
            var application_structure = new Mock<IApplicationStructure>();
            var string_format = new Mock<IStringFormat>();
            var segment_matches = new Mock<ISegmentMatches>();
            segment_matches.SetupGet(s => s.HasMatches).Returns(true);
            string_format.Setup(s => s.Match(typeof(Implementation).Namespace)).Returns(segment_matches.Object);

            application_structure.Setup(a => a.GetStructureFormatsForArea(area)).Returns(new[]
            {
                string_format.Object
            });

            application.SetupGet(a => a.Structure).Returns(application_structure.Object);


            type_discoverer.Setup(t => t.FindMultiple(typeof(IInterface))).Returns(new[] { typeof(Implementation) });
        };

        Because of = () => result = resolver.Resolve(identifier.Object);

        It should_return_expected_type = () => result.ShouldEqual(typeof(Implementation));
    }
}
