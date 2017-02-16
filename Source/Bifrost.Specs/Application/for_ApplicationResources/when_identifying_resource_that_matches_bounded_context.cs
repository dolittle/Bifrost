using System.Collections.Generic;
using System.Linq;
using Bifrost.Application;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_ApplicationResources
{
    public class when_identifying_resource_that_matches_bounded_context : given.application_resources_with_one_structure_format
    {
        const string BoundedContext = "MyBoundedContext";

        static ApplicationResourceIdentifier identifier;
        static Mock<ISegmentMatches> matches;
        static Mock<ISegmentMatch> bounded_context_match;

        Establish context = () =>
        {
            bounded_context_match = new Mock<ISegmentMatch>();
            bounded_context_match.SetupGet(b => b.Identifier).Returns(ApplicationResources.BoundedContextKey);
            bounded_context_match.SetupGet(b => b.Values).Returns(new[] { BoundedContext }); 

            var segments = new List<ISegmentMatch>(new[]
            {
                bounded_context_match.Object
            });

            matches = new Mock<ISegmentMatches>();
            matches.SetupGet(m => m.HasMatches).Returns(true);
            matches.Setup(m => m.GetEnumerator()).Returns(segments.GetEnumerator());

            string_format.Setup(s => s.Match(typeof(string).Namespace)).Returns(matches.Object);
        };

        Because of = () => identifier = resources.Identify("something");

        It should_have_one_segment = () => identifier.LocationSegments.Count().ShouldEqual(1);
        It should_have_bounded_context_as_first_segment = () => identifier.LocationSegments.ToArray()[0].ShouldBeOfExactType<BoundedContext>();
        It should_hold_the_correct_name_for_bounded_context = () => identifier.LocationSegments.ToArray()[0].Name.AsString().ShouldEqual(BoundedContext);
        It should_hold_the_application = () => identifier.Application.ShouldEqual(application.Object);
        It should_hold_the_type_as_application_resource = () => identifier.Resource.Name.AsString().ShouldEqual(typeof(string).Name);
    }
}
