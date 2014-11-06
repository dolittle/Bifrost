using Bifrost.Validation.MetaData;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ValidationMetaData.given
{
    public class validation_meta_data_with_two_generators : two_generators
    {
        protected static ValidationMetaData validation_meta_data;

        Establish context = () => validation_meta_data = new ValidationMetaData(generators_mock.Object);
    }
}
