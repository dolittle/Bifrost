using System;
using Bifrost.Applications;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationResourceTypes
{
    public class when_getting_for_unknown_type : given.one_resource_type
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => resource_types.GetFor(typeof(string)));

        It should_throw_unknown_application_resource_type = () => exception.ShouldBeOfExactType<UnknownApplicationResourceType>();
    }
}
