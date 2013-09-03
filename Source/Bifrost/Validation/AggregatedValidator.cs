using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Bifrost.Extensions;

namespace Bifrost.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class AggregatedValidator : ICanValidate
    {
        /// <summary>
        /// Validators
        /// </summary>
        public readonly IEnumerable<ICanValidate> Validators;

        /// <summary>
        /// An aggregated validator that can contain multiple inner validators.
        /// </summary>
        /// <param name="validators"></param>
        public AggregatedValidator(IEnumerable<ICanValidate> validators)
        {
            Validators = validators;
        }

        /// <summary>
        /// Validates that the object is in a valid state.
        /// </summary>
        /// <param name="target">The target to validate</param>
        /// <returns>A collection of ValidationResults.  An empty collection indicates a valid command.</returns>
        public IEnumerable<ValidationResult> ValidateFor(object target)
        {
            var result = new List<ValidationResult>();
            foreach (var validator in Validators)
            {
                validator.ValidateFor(target).ForEach(result.Add);
            }
            return result;
        }
    }
}
