using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_TypeExtensions
{
    public class when_asking_a_type_with_a_default_constructor_if_it_has_a_default_constructor
    {
        static bool result;

        Because of = () => result = typeof(TypeWithDefaultConstructor).HasDefaultConstructor();

        It should_return_true = () => result.ShouldBeTrue();
    }
}
