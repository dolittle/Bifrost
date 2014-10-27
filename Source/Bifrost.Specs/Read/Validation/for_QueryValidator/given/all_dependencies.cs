using Bifrost.Read.Validation;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Read.Validation.for_QueryValidator.given
{
    public class all_dependencies
    {
        protected static Mock<IQueryValidationDescriptors> query_validation_descriptors_mock;

        Establish context = () => query_validation_descriptors_mock = new Mock<IQueryValidationDescriptors>();
    }
}
