using Machine.Specifications;

namespace Bifrost.Specs.Read.Validation.for_QueryValidationDescriptors
{
    public class when_asking_if_has_descriptor_and_one_descriptor_exist : given.one_query_validation_descriptor_for_query
    {
        static bool result;

        Because of = () => result = descriptors.HasDescriptorFor<SimpleQuery>();

        It should_have = () => result.ShouldBeTrue();
    }
}
