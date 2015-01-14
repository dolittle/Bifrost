using System.Linq;
using Bifrost.Mapping;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_MappingTargets
{
    public class when_getting_for_type_without_known_target : given.no_mapping_targets
    {
        static IMappingTarget result;

        Because of = () => result = targets.GetFor(typeof(string));

        It should_return_the_default_mapping_target = () => result.ShouldBeOfExactType<DefaultMappingTarget>();
    }
}
