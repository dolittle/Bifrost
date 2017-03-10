using System.Linq;
using Bifrost.Applications;
using Bifrost.Strings;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifierConverter
{
    public class when_converting_string_identifier_with_bounded_context_and_module : given.an_application_resource_identifier_converter
    {
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string resource_name = "MyResource";

        static string string_identifier =
            $"{application_name}{ApplicationResourceIdentifierConverter.ApplicationSeparator}" +
            $"{bounded_context_name}{ApplicationResourceIdentifierConverter.ApplicationLocationSeparator}" +
            $"{module_name}" +
            $"{ApplicationResourceIdentifierConverter.ApplicationResourceSeparator}{resource_name}";

        static ApplicationResourceIdentifier identifier;

        Because of = () => identifier = converter.FromString(string_identifier);

        It should_return_a_matching_identifier = () => identifier.ShouldNotBeNull();
        It should_hold_the_application = () => identifier.Application.ShouldEqual(application.Object);
        It should_hold_the_resource = () => identifier.Resource.Name.Value.ShouldEqual(resource_name);
        It should_hold_the_two_segments = () => identifier.LocationSegments.Count().ShouldEqual(2);
        It should_hold_the_bounded_context_segment = () => identifier.LocationSegments.First().Name.AsString().ShouldEqual(bounded_context_name);
        It should_hold_the_module_segment = () => identifier.LocationSegments.ToArray()[1].Name.AsString().ShouldEqual(module_name);
    }
}
