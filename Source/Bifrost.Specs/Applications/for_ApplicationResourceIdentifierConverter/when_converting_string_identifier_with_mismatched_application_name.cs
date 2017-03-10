using System;
using Bifrost.Applications;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifierConverter
{
    public class when_converting_string_identifier_with_mismatched_application_name : given.an_application_resource_identifier_converter
    {
        const string bounded_context_name = "TheBoundedContext";
        const string module_name = "TheModule";
        const string feature_name = "TheFeature";
        const string sub_feature_name = "TheSubFeature";
        const string second_level_sub_feature_name = "TheSecondLevelSubFeature";
        const string resource_name = "MyResource";

        static string string_identifier =
            $"OtherApplication{ApplicationResourceIdentifierConverter.ApplicationSeparator}" +
            $"{bounded_context_name}{ApplicationResourceIdentifierConverter.ApplicationLocationSeparator}" +
            $"{ApplicationResourceIdentifierConverter.ApplicationResourceSeparator}{resource_name}";

        static Exception exception;

        Because of = () => exception = Catch.Exception(() => converter.FromString(string_identifier));

        It should_throw_application_mismatch = () => exception.ShouldBeOfExactType<ApplicationMismatch>();
    }
}
