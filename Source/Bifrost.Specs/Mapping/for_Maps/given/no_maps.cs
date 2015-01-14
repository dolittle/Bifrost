using Bifrost.Mapping;
using Machine.Specifications;
using System.Linq;

namespace Bifrost.Specs.Mapping.for_Maps.given
{
    public class no_maps : all_dependencies
    {
        protected static Maps maps;

        Establish context = () =>
        {
            map_instances_mock.Setup(m => m.GetEnumerator()).Returns(new IMap[0].AsEnumerable().GetEnumerator());
            maps = new Maps(map_instances_mock.Object);
        };
    }
}
