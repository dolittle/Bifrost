using System;
using Bifrost.Validation;
using FluentValidation;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Validation.for_RuleBuilderExtensions
{
    public class when_adding_dynamic_state_to_validator_not_capable_of_dynamic_state
    {
        static Mock<AbstractValidator<object>> validator;
        static Exception exception;

        Establish context = () => validator = new Mock<AbstractValidator<object>>();

        Because of = () => exception = Catch.Exception(() => validator.Object.RuleFor(o => o).NotNull().WithDynamicStateFrom(o => o));

        It should_throw_invalid_validator_type_exception = () => exception.ShouldBeOfType<InvalidValidatorTypeException>();
    }
}
