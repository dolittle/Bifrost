using Bifrost.Applications;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResources
{
    public class when_converting_identifier_with_five_segments_for_a_resource_to_string : given.application_resources_without_structure_formats
    {
        const string application_name = "MyApplication";
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string resource_name = "MyResource";

        static string expected = 
            $"{application_name}{ApplicationResources.ApplicationSeparator}"+
            $"{bounded_context_name}{ApplicationResources.ApplicationLocationSeparator}"+
            $"{module_name}{ApplicationResources.ApplicationLocationSeparator}" +
            $"{feature_name}{ApplicationResources.ApplicationLocationSeparator}" +
            $"{sub_feature_name}{ApplicationResources.ApplicationLocationSeparator}" +
            $"{second_level_sub_feature_name}" +
            $"{ApplicationResources.ApplicationResourceSeparator}{resource_name}";

        static ApplicationResourceIdentifier identifier;

        static string result;

        Establish context = () =>
        {
            var application = new Mock<IApplication>();
            application.SetupGet(a => a.Name).Returns(application_name);
            var bounded_context = new BoundedContext(bounded_context_name);
            var module = new Module(bounded_context, module_name);
            var feature = new Feature(module, feature_name);
            var sub_feature = new SubFeature(feature, sub_feature_name);
            var second_level_sub_feature = new SubFeature(sub_feature, second_level_sub_feature_name);
            var resource = new ApplicationResource(resource_name);

            identifier = new ApplicationResourceIdentifier(application.Object,new IApplicationLocation[] {
                bounded_context,
                module,
                feature,
                sub_feature,
                second_level_sub_feature
            }, resource);
        };

        Because of = () => result = resources.AsString(identifier);

        It should_return_expected_string = () => result.ShouldEqual(expected);
    }
}
