using System.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_PropertyValidatorWithDynamicState
{
    public class when_adding_property_expression
    {
        static MyValidator    validator;
        
        Establish context = () => validator = new MyValidator();

        Because of = () => validator.AddExpression<MyValidator>(v => v.Something);

        It should_add_property_for_the_expression = () => validator.Properties.First().Name.ShouldEqual("Something");
    }
}
