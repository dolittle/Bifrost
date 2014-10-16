using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Combines multiples validators into a single validator
    /// </summary>
    /// <typeparam name="T">Type that the composite validator validates</typeparam>
    public class ComposedValidator<T> : AbstractValidator<T>
    {
        readonly List<IValidator> registeredValidators = new List<IValidator>();

        /// <summary>
        /// Instantiates an instance of a <see cref="ComposedValidator{T}"/>
        /// </summary>
        /// <param name="validators"></param>
        public ComposedValidator(IEnumerable<IValidator> validators)
        {
            registeredValidators.AddRange(validators);
        }

        /// <summary>
        /// Validates the ValidationContext
        /// </summary>
        /// <param name="context">Context of the validation</param>
        /// <returns>ValidationResult</returns>
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var errors = registeredValidators.SelectMany(x => x.Validate(context).Errors);
            return new ValidationResult(errors);
        }
    }
}