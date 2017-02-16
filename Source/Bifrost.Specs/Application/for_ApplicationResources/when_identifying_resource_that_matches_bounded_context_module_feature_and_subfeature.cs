using System.Collections.Generic;
using System.Linq;
using Bifrost.Application;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_ApplicationResources
{
    public class when_identifying_resource_that_matches_bounded_context_module_feature_and_subfeature : given.application_resources_with_one_structure_format
    {
        const string BoundedContext = "MyBoundedContext";
        const string Module = "MyModule";
        const string Feature = "MyFeature";
        const string TopLevelSubFeature = "MyTopLevelSubFeature";
        const string SecondLevelSubFeature = "MySecondLevelSubFeature";

        static ApplicationResourceIdentifier identifier;
        static Mock<ISegmentMatches> matches;
        static Mock<ISegmentMatch> bounded_context_match;
        static Mock<ISegmentMatch> module_match;
        static Mock<ISegmentMatch> feature_match;
        static Mock<ISegmentMatch> subfeature_match;

        Establish context = () =>
        {
            bounded_context_match = new Mock<ISegmentMatch>();
            bounded_context_match.SetupGet(b => b.Identifier).Returns(ApplicationResources.BoundedContextKey);
            bounded_context_match.SetupGet(b => b.Values).Returns(new[] { BoundedContext }); 

            module_match = new Mock<ISegmentMatch>();
            module_match.SetupGet(b => b.Identifier).Returns(ApplicationResources.ModuleKey);
            module_match.SetupGet(b => b.Values).Returns(new[] { Module });

            feature_match = new Mock<ISegmentMatch>();
            feature_match.SetupGet(b => b.Identifier).Returns(ApplicationResources.FeatureKey);
            feature_match.SetupGet(b => b.Values).Returns(new[] { Feature });

            subfeature_match = new Mock<ISegmentMatch>();
            subfeature_match.SetupGet(b => b.Identifier).Returns(ApplicationResources.SubFeatureKey);
            subfeature_match.SetupGet(b => b.Values).Returns(new[] { TopLevelSubFeature, SecondLevelSubFeature });

            var segments = new List<ISegmentMatch>(new[]
            {
                bounded_context_match.Object,
                module_match.Object,
                feature_match.Object,
                subfeature_match.Object,
                
            });

            matches = new Mock<ISegmentMatches>();
            matches.SetupGet(m => m.HasMatches).Returns(true);
            matches.Setup(m => m.GetEnumerator()).Returns(segments.GetEnumerator());

            string_format.Setup(s => s.Match(typeof(string).Namespace)).Returns(matches.Object);
        };

        Because of = () => identifier = resources.Identify("something");

        It should_have_five_segments = () => identifier.LocationSegments.Count().ShouldEqual(5);
        It should_have_bounded_context_as_first_segment = () => identifier.LocationSegments.ToArray()[0].ShouldBeOfExactType<BoundedContext>();
        It should_have_module_as_second_segment = () => identifier.LocationSegments.ToArray()[1].ShouldBeOfExactType<Module>();
        It should_have_feature_as_third_segment = () => identifier.LocationSegments.ToArray()[2].ShouldBeOfExactType<Feature>();
        It should_have_subfeature_as_fourth_segment = () => identifier.LocationSegments.ToArray()[3].ShouldBeOfExactType<SubFeature>();
        It should_have_subfeature_as_fifth_segment = () => identifier.LocationSegments.ToArray()[4].ShouldBeOfExactType<SubFeature>();
        It should_hold_the_correct_name_for_bounded_context = () => identifier.LocationSegments.ToArray()[0].Name.AsString().ShouldEqual(BoundedContext);
        It should_hold_the_correct_name_for_module = () => identifier.LocationSegments.ToArray()[1].Name.AsString().ShouldEqual(Module);
        It should_hold_the_correct_name_for_feature = () => identifier.LocationSegments.ToArray()[2].Name.AsString().ShouldEqual(Feature);
        It should_hold_the_correct_name_for_toplevel_subfeature= () => identifier.LocationSegments.ToArray()[3].Name.AsString().ShouldEqual(TopLevelSubFeature);
        It should_hold_the_correct_name_for_secondlevel_subfeature = () => identifier.LocationSegments.ToArray()[4].Name.AsString().ShouldEqual(SecondLevelSubFeature);
        It should_hold_the_application = () => identifier.Application.ShouldEqual(application.Object);
        It should_hold_the_type_as_application_resource = () => identifier.Resource.Name.AsString().ShouldEqual(typeof(string).Name);
    }
}
