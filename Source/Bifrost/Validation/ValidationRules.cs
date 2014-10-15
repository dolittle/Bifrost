#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using Bifrost.Rules;
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
            builder.AddRule(new Email());
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
            builder.AddRule(new GreaterThan<T>(value));
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
            builder.AddRule(new GreaterThanOrEqual<T>(value));
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
            builder.AddRule(new MaxLength(length));
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
            builder.AddRule(new LessThan<T>(value));
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
            builder.AddRule(new LessThanOrEqual<T>(value));
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
            builder.AddRule(new Regex(expression));
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
            builder.AddRule(new Required());
            return builder;
        }
    }
}
