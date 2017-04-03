using Bifrost.Applications;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationResourceTypes
{
    public class when_getting_for_known_identifier : given.one_resource_type
    {
        static IApplicationResourceType result;

        Because of = () => result = resource_types.GetFor(identifier);

        It should_return_the_resource_type = () => result.ShouldEqual(resource_type.Object);
    }
}
