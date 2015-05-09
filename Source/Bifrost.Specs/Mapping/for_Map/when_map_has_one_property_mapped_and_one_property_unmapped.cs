using System.Linq;
using Bifrost.Mapping;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_Map
{
    public class when_map_has_one_property_mapped_and_one_property_unmapped
    {
        static MapWithOneOfTwoPropertiesMapped map;

        Because of = () => map = new MapWithOneOfTwoPropertiesMapped();

        It should_map_with_the_source_property_strategy = () => map.Properties.ToArray()[1].Strategy.ShouldBeOfExactType<SourcePropertyMappingStrategy>();
    }
}
