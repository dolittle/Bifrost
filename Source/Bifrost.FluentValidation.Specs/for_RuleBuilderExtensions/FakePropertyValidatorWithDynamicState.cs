using Bifrost.Validation;
using FluentValidation.Validators;
using System.Linq.Expressions;
using System;

namespace Bifrost.Specs.Validation.for_RuleBuilderExtensions
{
    public class FakePropertyValidatorWithDynamicState : PropertyValidatorWithDynamicState
    {

        public FakePropertyValidatorWithDynamicState() : base("") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return true;
        }


        public bool AddExpressionCalled = false;

        public override void AddExpression<T>(Expression<Func<T, object>> expression)
        {
            AddExpressionCalled = true;
        }
    }
}
