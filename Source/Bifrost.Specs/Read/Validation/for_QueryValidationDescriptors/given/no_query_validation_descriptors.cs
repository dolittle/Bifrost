using System;
using Bifrost.Read.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidationDescriptors.given
{
    public class no_query_validation_descriptors : given.all_dependencies
    {
        protected static QueryValidationDescriptors descriptors;

        Establish context = () =>
        {
            type_discoverer_mock.Setup(t => t.FindMultiple(typeof(QueryValidationDescriptorFor<>))).Returns(new Type[0]);
            descriptors = new QueryValidationDescriptors(type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
