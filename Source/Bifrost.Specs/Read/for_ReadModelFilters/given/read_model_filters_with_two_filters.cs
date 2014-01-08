using Bifrost.Read;
using Machine.Specifications;

namespace Bifrost.Specs.Read.for_ReadModelFilters.given
{
    public class read_model_filters_with_two_filters : all_dependencies
    {
        protected static ReadModelFilters   filters;

        Establish context = () =>
        {

            //type_discoverer_mock.Setup(t=>t.FindMultiple<ICanFilterReadModels>()).Returns(

            filters = new ReadModelFilters(type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
