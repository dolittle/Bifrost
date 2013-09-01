using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Commands;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a command business validator that is constructed from discovered rules.
    /// </summary>
    public class DynamicCommandBusinessValidator<T> : BusinessValidator<T>, ICanValidate<T>, ICommandBusinessValidator where T : class, ICommand
    {
        /// <summary>
        /// Instantiates an Instance of a <see cref="DynamicCommandBusinessValidator"/>
        /// </summary>
        /// <param name="validators">A collection of dynamically discovered validators to use for the property type</param>
        public DynamicCommandBusinessValidator(IDictionary<Type,IEnumerable<BusinessValidator<IAmValidatable>>> propertyTypesAndValidators)
        {
            //TODO: Map up the propety types to properties on the command
        } 

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<ValidationResult> ValidateFor(ICommand command)
        {
            return new ValidationResult[0];
        }

        public IEnumerable<ValidationResult> ValidateFor(T target)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<ValidationResult> ValidateFor(object target)
        {
            return new ValidationResult[0];
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor(target);
        }
#pragma warning restore 1591 // Xml Comments
    }
}