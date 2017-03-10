using System;
using Bifrost.Applications;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResources
{
    public class when_converting_string_identifier_missing_application_locations_in_string : given.application_resources_without_structure_formats
    {
        static string string_identifier = $"{application_name}{ApplicationResources.ApplicationSeparator}{ApplicationResources.ApplicationResourceSeparator}Resource";
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => resources.FromString(string_identifier));

        It should_throw_missing_application_locations = () => exception.ShouldBeOfExactType<MissingApplicationLocations>();
    }
}
