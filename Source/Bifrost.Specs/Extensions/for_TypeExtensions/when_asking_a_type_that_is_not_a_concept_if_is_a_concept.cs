using Machine.Specifications;
using Bifrost.Extensions;

namespace Bifrost.Specs.Extensions.for_TypeExtensions
{
    public class when_asking_a_type_that_is_not_a_concept_if_is_a_concept
    {
        static bool result;

        Because of = () => result = typeof(object).IsConcept();

        It should_return_false = () => result.ShouldBeFalse();
    }
}
