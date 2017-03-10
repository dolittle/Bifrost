using System;
using Bifrost.Applications;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResources
{
    public class when_converting_string_identifier_missing_application_separator_in_string : given.application_resources_without_structure_formats
    {
        static string string_identifier = "Application";
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => resources.FromString(string_identifier));

        It should_throw_unable_to_identify_application = () => exception.ShouldBeOfExactType<UnableToIdentifyApplication>();
    }
}
