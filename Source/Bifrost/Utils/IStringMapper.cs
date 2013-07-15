#region License
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

using System.Collections.Generic;

namespace Bifrost.Utils
{
    /// <summary>
    /// Defines a system for mapping strings that picks the first <see cref="IStringMapping"/> that matches
    /// </summary>
    public interface IStringMapper
    {
        /// <summary>
        /// Indicates whether the mapper holds a mapping that matches this string
        /// </summary>
        /// <param name="input">string to check for mapper</param>
        /// <returns>true if it has at least one matching mapper, false otherwise</returns>
        bool HasMappingFor(string input);

        /// <summary>
        /// Gets the first mapper that matches the inputted string
        /// </summary>
        /// <param name="input">string to match against</param>
        /// <returns>The first matching <see cref="IStringMapping"/> for the inputted string</returns>
        IStringMapping GetFirstMatchingMappingFor(string input);

        /// <summary>
        /// Returns all mappers that can resolve the inputted string
        /// </summary>
        /// <param name="input">string to match against</param>
        /// <returns>All <see cref="IStringMapping"/> that match the inputted string</returns>
        IEnumerable<IStringMapping> GetAllMatchingMappingsFor(string input);

        /// <summary>
        /// Resolves the inputted string using the first matching mapper
        /// </summary>
        /// <param name="input">string to resolve</param>
        /// <returns>the resolved string</returns>
        string Resolve(string input);

        /// <summary>
        /// Adds an <see cref="IStringMapping"/> to the mappings
        /// </summary>
        /// <param name="format">Format to map from</param>
        /// <param name="mappedFormat">Format to map to</param>
        void AddMapping(string format, string mappedFormat);

        /// <summary>
        /// Adds an <see cref="IStringMapping"/> to the mappings
        /// </summary>
        /// <param name="mapping">The mapping to add</param>
        void AddMapping(IStringMapping mapping);
    }
}
