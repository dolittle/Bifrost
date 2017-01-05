/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Validation.Rules;

namespace Bifrost.Validation
{
    /// <summary>
    /// Extensions for <see cref="ValueValidationBuilder"/>
    /// </summary>
    public static class ValidationRules
    {
        /// <summary>
        /// Value must be a valid email
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        public static ValueValidationBuilder<string> MustBeValidEMail(this ValueValidationBuilder<string> builder)
        {
            builder.AddRule(new Email(builder.Property));
            return builder;
        }

        /// <summary>
        /// Value must be greater than
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <param name="value">Value the input value must be greater than</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        public static ValueValidationBuilder<T> HasToBeGreaterThan<T>(this ValueValidationBuilder<T> builder, T value)
            where T:IComparable<T>
        {
            builder.AddRule(new GreaterThan<T>(builder.Property, value));
            return builder;
        }

        /// <summary>
        /// Value must be greater than or equal
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <param name="value">Value the input value must be greater or equal than</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        public static ValueValidationBuilder<T> HasToBeGreaterThanOrEqual<T>(this ValueValidationBuilder<T> builder, T value)
            where T : IComparable<T>
        {
            builder.AddRule(new GreaterThanOrEqual<T>(builder.Property, value));
            return builder;
        }

        /// <summary>
        /// String must be a specific length
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <param name="length">The length the value must be</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        public static ValueValidationBuilder<string> MustHaveMaxLengthOf(this ValueValidationBuilder<string> builder, int length)
        {
            builder.AddRule(new MaxLength(builder.Property, length));
            return builder;
        }

        /// <summary>
        /// Value must be less than
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <param name="value">Value the input value must be less than</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        public static ValueValidationBuilder<T> HasToBeLessThan<T>(this ValueValidationBuilder<T> builder, T value)
            where T : IComparable<T>
        {
            builder.AddRule(new LessThan<T>(builder.Property, value));
            return builder;
        }

        /// <summary>
        /// Value must be less than or equal
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <param name="value">Value the input value must be less or equal than</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        public static ValueValidationBuilder<T> HasToBeLessThanOrEqual<T>(this ValueValidationBuilder<T> builder, T value)
            where T : IComparable<T>
        {
            builder.AddRule(new LessThanOrEqual<T>(builder.Property, value));
            return builder;
        }

        /// <summary>
        /// Value must conform to a specific regular expression
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <param name="expression">The regular expression that the value must conform to</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        public static ValueValidationBuilder<string> MustConformToRegularExpressionOf(this ValueValidationBuilder<string> builder, string expression)
        {
            builder.AddRule(new Regex(builder.Property, expression));
            return builder;
        }

        /// <summary>
        /// Value is required
        /// </summary>
        /// <param name="builder"><see cref="ValueValidationBuilder"/> to build for</param>
        /// <returns><see cref="ValueValidationBuilder"/> to continue building on</returns>
        /// <remarks>
        /// By required it means that it can't be the default value of the type. 
        /// </remarks>
        public static IValueValidationBuilder IsRequired(this IValueValidationBuilder builder)
        {
            builder.AddRule(new Required(builder.Property));
            return builder;
        }
    }
}
