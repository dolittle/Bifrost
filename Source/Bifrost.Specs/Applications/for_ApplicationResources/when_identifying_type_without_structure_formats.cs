using System;
using Bifrost.Applications;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationResources
{
    public class when_identifying_type_without_structure_formats : given.application_resources_without_structure_formats
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => resources.Identify(typeof(string)));

        It should_throw_unable_to_identify_application_resource = () => exception.ShouldBeOfExactType<UnableToIdentifyApplicationResource>();
    }
}
