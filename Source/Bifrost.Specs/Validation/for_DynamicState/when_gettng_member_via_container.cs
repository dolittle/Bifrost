using Bifrost.Validation;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_DynamicState
{
    public class when_gettng_member_via_container
    {
        static ModelContainer container;
        static Model model;
        static dynamic state;
        static string result;

        Establish context = () =>
        {
            container = new ModelContainer();
            model = new Model();
            container.Model = model;
            state = new DynamicState(container, new[] { Model.TheStringProperty });
        };

        Because of = () => result = state.TheString;

        It should_call_get_on_model = () => model.TheStringGetCalled.ShouldBeTrue();
    }
}
