using Bifrost.Mapping;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_MappingTargetFor
{
    public class when_asking_for_target_type
    {
        static StringMappingTarget target;

        Establish context = () => target = new StringMappingTarget();

        It should_return_the_type_given_as_generic_parameter = () => target.TargetType.ShouldEqual(typeof(string));
    }
}
