using System.Linq;
using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResources
{
    public class when_converting_string_identifier_with_bounded_context_and_module : given.application_resources_without_structure_formats
    {
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string resource_name = "MyResource";

        static string string_identifier =
            $"{application_name}{ApplicationResources.ApplicationSeparator}" +
            $"{bounded_context_name}{ApplicationResources.ApplicationLocationSeparator}" +
            $"{module_name}" +
            $"{ApplicationResources.ApplicationResourceSeparator}{resource_name}";

        static ApplicationResourceIdentifier identifier;
        static ApplicationResources resources;
        static Mock<IStringFormat> string_format;

        Establish context = () =>
        {
            string_format = new Mock<IStringFormat>();
            application_structure.SetupGet(a => a.AllStructureFormats).Returns(new[] { string_format.Object });
            resources = new ApplicationResources(application.Object);
        };

        Because of = () => identifier = resources.FromString(string_identifier);

        It should_return_a_matching_identifier = () => identifier.ShouldNotBeNull();
        It should_hold_the_application = () => identifier.Application.ShouldEqual(application.Object);
        It should_hold_the_resource = () => identifier.Resource.Name.Value.ShouldEqual(resource_name);
        It should_hold_the_two_segments = () => identifier.LocationSegments.Count().ShouldEqual(2);
        It should_hold_the_bounded_context_segment = () => identifier.LocationSegments.First().Name.AsString().ShouldEqual(bounded_context_name);
        It should_hold_the_module_segment = () => identifier.LocationSegments.ToArray()[1].Name.AsString().ShouldEqual(module_name);
    }
}
