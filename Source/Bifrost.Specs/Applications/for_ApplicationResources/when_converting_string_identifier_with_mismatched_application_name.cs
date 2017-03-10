using System;
using System.Linq;
using Bifrost.Applications;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResources
{
    public class when_converting_string_identifier_with_mismatched_application_name : given.application_resources_without_structure_formats
    {
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string resource_name = "MyResource";

        static string string_identifier =
            $"OtherApplication{ApplicationResources.ApplicationSeparator}" +
            $"{bounded_context_name}{ApplicationResources.ApplicationLocationSeparator}" +
            $"{ApplicationResources.ApplicationResourceSeparator}{resource_name}";

        static Exception exception;

        Because of = () => exception = Catch.Exception(() => resources.FromString(string_identifier));

        It should_throw_application_mismatch = () => exception.ShouldBeOfExactType<ApplicationMismatch>();
    }
}
