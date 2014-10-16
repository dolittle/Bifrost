using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.FluentValidation
{
    /// <summary>
    /// Base class to inherit from for basic input validation rules
    /// </summary>
    /// <typeparam name="T">Type to add validation rules for</typeparam>
    public class InputValidator<T> : AbstractValidator<T>, IValidateInput<T>
    {
        
    }
}