#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Internal;
using FluentValidation.Validators;

namespace Bifrost.FluentValidation.MetaData
{
    /// <summary>
    /// Provides extensions for formatting messages from property validator
    /// </summary>
    public static class PropertyValidatorMessageFormattingExtensions
    {
        /// <summary>
        /// Get a error message for a property
        /// </summary>
        /// <param name="propertyValidator">PropertyValidator to get for</param>
        /// <param name="propertyName">Name of propery</param>
        /// <returns>Formatted string</returns>
        public static string GetErrorMessageFor(this IPropertyValidator propertyValidator, string propertyName)
        {
            var formatter = new MessageFormatter().AppendPropertyName(propertyName);
            var errorMessage = formatter.BuildMessage(propertyValidator.ErrorMessageSource.GetString());
            return errorMessage;
        }
    }
}
