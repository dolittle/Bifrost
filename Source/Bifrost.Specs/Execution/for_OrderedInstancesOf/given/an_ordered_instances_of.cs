using System.Linq;
using Bifrost.Execution;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Execution.for_OrderedInstancesOf.given
{
    public class an_ordered_instances_of
    {
        public interface IDummy { }

        protected static Mock<ITypeDiscoverer> type_discoverer_mock;
        protected static Mock<IContainer> container_mock;
        protected static OrderedInstancesOf<IDummy> ordered_instances_of;

        protected static IDummy[] result;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            container_mock = new Mock<IContainer>();
        };

        protected static void Register(params IDummy[] instances)
        {
            type_discoverer_mock
                .Setup(m => m.FindMultiple<IDummy>())
                .Returns(instances.Select(i => i.GetType()));
            foreach (var instance in instances)
            {
                container_mock
                    .Setup(m => m.Get(instance.GetType()))
                    .Returns(instance);
            }

            ordered_instances_of = new OrderedInstancesOf<IDummy>(type_discoverer_mock.Object, container_mock.Object);
        }
    }
}