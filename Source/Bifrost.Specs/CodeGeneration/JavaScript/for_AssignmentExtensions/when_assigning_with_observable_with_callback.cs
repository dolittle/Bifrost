using Machine.Specifications;
using Bifrost.CodeGeneration.JavaScript;

namespace Bifrost.Specs.CodeGeneration.JavaScript.for_AssignmentExtensions
{
    public class when_assigning_with_observable_with_callback : given.an_assignment
    {
        static Observable observable;

        Because of = () => assignment.WithObservable(o => observable = o);

        It should_set_value_to_be_an_observable = () => assignment.Value.ShouldBeOfType<Observable>();
        It should_call_the_callback = () => observable.ShouldNotBeNull();
    }
}
