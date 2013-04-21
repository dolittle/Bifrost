using Bifrost.Web.Mvc.Validation;
using Machine.Specifications;
using System.Web.Mvc;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.Validation.for_ValidatorPropertyValidator
{
    public class when_creating_with_validator_with_dynamic_state
    {
        const string expected = "42";
        static Model model;
        static Mock<ControllerBase> controller_mock;
        static ViewDataDictionary<Model> view_data;
        static ValidatorPropertyValidator  property_validator;
        static MyValidator validator;

        Establish context = () =>
        {
            validator = new MyValidator();
            validator.AddExpression<Model>(m => m.TheString);
            model = new Model();
            model.TheString = expected;
            view_data = new ViewDataDictionary<Model>(model);
            controller_mock = new Mock<ControllerBase>();
            controller_mock.Object.ViewData = view_data;
            property_validator = new ValidatorPropertyValidator(
                    ModelMetadata.FromLambdaExpression<Model,string>(m=>m.TheString, view_data),
                    new ControllerContext
                    {
                        Controller = controller_mock.Object
                    },
                    null,
                    validator
                );
        };

        It should_have_dynamic_state = () => ShouldExtensionMethods.ShouldNotBeNull(property_validator.DynamicState);
        It should_have_the_property_in_dynamic_state = () => ShouldExtensionMethods.ShouldEqual(property_validator.DynamicState.TheString, expected);
    }
}
