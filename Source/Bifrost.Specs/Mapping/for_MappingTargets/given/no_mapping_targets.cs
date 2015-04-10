using Bifrost.Mapping;
using Machine.Specifications;
using System.Linq;

namespace Bifrost.Specs.Mapping.for_MappingTargets.given
{
    public class no_mapping_targets : all_dependencies
    {
        protected static MappingTargets targets;

        Establish context = () =>
        {
            mapping_targets_mock.Setup(m => m.GetEnumerator()).Returns(new IMappingTarget[0].AsEnumerable().GetEnumerator());
            targets = new MappingTargets(mapping_targets_mock.Object);
        };
    }
}
