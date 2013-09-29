using FluentValidation;

namespace Bifrost.Validation
{
    /// <summary>
    /// Base class to inherit from for basic input validation rules
    /// </summary>
    /// <typeparam name="T">Type to add validation rules for</typeparam>
    public class InputValidator<T> : AbstractValidator<T>, IValidateInput<T>
    {
        
    }
}