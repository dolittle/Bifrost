using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Commands;
using System.Linq;
using FluentValidation;
using Bifrost.Extensions;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a command business validator that is constructed from discovered rules.
    /// </summary>
    public class DynamicCommandInputValidator<T> : InputValidator<T>, ICanValidate<T>, ICommandInputValidator where T : class, ICommand
    {
        /// <summary>
        /// Instantiates an Instance of a <see cref="DynamicCommandInputValidator"/>
        /// </summary>
        /// <param name="propertyTypesAndValidators">A collection of dynamically discovered validators to use</param>
        public DynamicCommandInputValidator(IDictionary<Type,IEnumerable<IValidator>> propertyTypesAndValidators)
        {
            foreach (var propertyType in propertyTypesAndValidators.Keys)
            {
                var validators = propertyTypesAndValidators[propertyType];

                if (validators != null && validators.Any())
                {
                    var validator = GetValidator(validators);

                    var properties = GetPropertiesWithType(propertyType);
                    foreach (var property in properties)
                    {
                        var expression = BuildGetExpression(property);
                        RuleFor(expression)
                            .DynamicValidationRule(validator, property.Name);
                    }
                }
            }
        }

        IValidator GetValidator(IEnumerable<IValidator> propertyTypesAndValidator)
        {
            return new ValidatorWrapper<IAmValidatable>(propertyTypesAndValidator);
        }

        IEnumerable<PropertyInfo> GetPropertiesWithType(Type propertyType)
        {
            var commandType = typeof (T);
            var properties = commandType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == propertyType);
            return properties;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<ValidationResult> ValidateFor(ICommand command)
        {
            return ValidateFor(command);
        }

        public virtual IEnumerable<ValidationResult> ValidateFor(T command)
        {
            var result = Validate(command as T);
            return from error in result.Errors
                   select new ValidationResult(error.ErrorMessage, new[] { error.PropertyName });
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }
#pragma warning restore 1591 // Xml Comments

        static Expression<Func<T, IAmValidatable>> BuildGetExpression(PropertyInfo propertyInfo)
        {
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            expr = Expression.Property(expr, propertyInfo);
            type = propertyInfo.PropertyType;

            return Expression.Lambda<Func<T, IAmValidatable>>(expr, arg);
        }


    }
}