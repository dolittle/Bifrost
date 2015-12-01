using System;
using Bifrost.Validation.MetaData;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation.MetaData
{
    /// <summary>
    /// Represents the generater that can generate a <see cref="Length"/> rule from
    /// an <see cref="ILengthValidator"/>
    /// </summary>
    public class LengthGenerator : ICanGenerateRule
    {
#pragma warning disable 1591 // Xml Comments
        public Type[] From { get { return new[] { typeof(ILengthValidator) }; } }

        public Rule GeneratorFrom(string propertyName, IPropertyValidator propertyValidator)
        {
            return new Length
                {
                    Min = ((ILengthValidator)propertyValidator).Min,
                    Max = ((ILengthValidator)propertyValidator).Max,
                    Message = propertyValidator.GetErrorMessageFor(propertyName)
                };
        }
#pragma warning restore 1591 // Xml Comments

    }
}