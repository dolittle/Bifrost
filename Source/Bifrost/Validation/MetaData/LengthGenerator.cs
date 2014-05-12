using System;
using FluentValidation.Validators;

namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="Length"/> rule from
    /// a <see cref="LengthValidator"/>
    /// </summary>
    public class LengthGenerator : ICanGenerateRule
    {
#pragma warning disable 1591 // Xml Comments
        public Type[] From { get { return new[] { typeof(LengthValidator) }; } }

        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new Length
                {
                    Min = ((LengthValidator)propertyValidator).Min,
                    Max = ((LengthValidator)propertyValidator).Max,
                    Message = propertyValidator.GetErrorMessageFor(propertyName)
                };
        }
#pragma warning restore 1591 // Xml Comments

    }
}