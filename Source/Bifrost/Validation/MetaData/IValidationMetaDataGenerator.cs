﻿#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using FluentValidation;

namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Defines the generator that generates metadata for a validator
    /// </summary>
    public interface IValidationMetaDataGenerator
    {
        /// <summary>
        /// Generate metadata from a specific validator
        /// </summary>
        /// <param name="validator">Validator to generate from</param>
        /// <returns>The actual metadata</returns>
        ValidationMetaData GenerateFrom(IValidator validator);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        ValidationMetaData GenerateFrom(AggregatedValidator validator);
    }
}
