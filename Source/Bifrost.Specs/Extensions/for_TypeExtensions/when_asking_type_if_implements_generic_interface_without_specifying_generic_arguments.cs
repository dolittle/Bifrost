using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_TypeExtensions
{

    public class when_asking_type_if_implements_generic_interface_without_specifying_generic_arguments
    {
        static bool result;

        Because of = () => result = typeof(ClassImplementingGenericInterface).HasInterface(typeof(IInterfaceWithGenericArguments<>));

        It should_have_the_interface = () => result.ShouldBeTrue();
    }
}
