using Bifrost.Applications;
using Machine.Specifications;

namespace Bifrost.Specs.Applications.for_ApplicationResourceTypes
{
    public class when_getting_for_known_type : given.one_resource_type
    {
        static IApplicationResourceType result;

        Because of = () => result = resource_types.GetFor(typeof(Implementation));

        It should_return_the_resource_type = () => result.ShouldEqual(resource_type.Object);
    }
}
