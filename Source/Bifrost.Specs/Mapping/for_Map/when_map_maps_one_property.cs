using System.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_Map
{
    public class when_map_maps_one_property
    {
        static MyMap map;

        Because of = () => map = new MyMap();

        It should_hold_one_mapped_property = () => map.Properties.Count().ShouldEqual(1);
        It should_hold_the_mapped_property = () => map.Properties.First().From.ShouldEqual(typeof(Source).GetProperty("SomeProperty"));
        It should_return_the_property_map = () => map.Properties.First().ShouldEqual(map.ReturnValueForProperty);
    }
}
