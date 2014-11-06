using Bifrost.Execution;
using Bifrost.Validation.MetaData;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Validation.for_ValidationMetaData.given
{
    public class all_dependencies
    {
        protected static Mock<IInstancesOf<ICanGenerateValidationMetaData>> generators_mock;

        Establish context = () => generators_mock = new Mock<IInstancesOf<ICanGenerateValidationMetaData>>();
    }
}
