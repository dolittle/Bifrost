using System;
using Bifrost.Read.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidationDescriptors.given
{
    public class one_query_validation_descriptor_for_query : given.all_dependencies
    {
        protected static QueryValidationDescriptors descriptors;
        protected static SimpleQueryDescriptor descriptor;

        Establish context = () =>
        {
            descriptor = new SimpleQueryDescriptor();
            type_discoverer_mock.Setup(t => t.FindMultiple(typeof(QueryValidationDescriptorFor<>))).Returns(new Type[] { typeof(SimpleQueryDescriptor) } );
            container_mock.Setup(c => c.Get(typeof(SimpleQueryDescriptor))).Returns(descriptor);
            descriptors = new QueryValidationDescriptors(type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
