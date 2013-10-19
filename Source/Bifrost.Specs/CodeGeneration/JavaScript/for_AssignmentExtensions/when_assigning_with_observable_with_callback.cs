using Machine.Specifications;
using Bifrost.CodeGeneration.JavaScript;

namespace Bifrost.Specs.CodeGeneration.JavaScript.for_AssignmentExtensions
{
    public class when_assigning_with_observable_with_callback : given.an_assignment
    {
        static string property_name;
        static Observable observable;

        Because of = () => assignment.WithObservable((n,o) => {
            property_name = n;
            observable = o;
        });

        It should_set_value_to_be_an_observable = () => assignment.Value.ShouldBeOfType<Observable>();
        It should_call_the_callback = () => observable.ShouldNotBeNull();
        It should_pass_the_property_name = () => property_name.ShouldEqual(assignment.Name);
    }
}
