using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidationDescriptors
{
    public class when_asking_if_has_descriptor_and_no_descriptors_exists : given.no_query_validation_descriptors
    {
        static bool result;

        Because of = () => result = descriptors.HasDescriptorFor<SimpleQuery>();

        It should_not_have_any = () => result.ShouldBeFalse();
    }
}
