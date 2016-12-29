using System.Linq;
using System.Reflection;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_Map
{
    public class when_map_maps_one_property
    {
        static MyMap map;

        Because of = () => map = new MyMap();

        It should_hold_the_mapped_property = () => map.Properties.First().From.ShouldEqual(typeof(Source).GetTypeInfo().GetProperty("SomeProperty"));
    }
}
