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
namespace Bifrost.Utils
{
    /// <summary>
    /// Defines a mapping for strings
    /// </summary>
    public interface IStringMapping
    {
        /// <summary>
        /// Gets the source format
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Gets the mapped format
        /// </summary>
        string MappedFormat { get; }

        /// <summary>
        /// Checks wether or not a particular input string matches the format for the mapping
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>True if matches, false it not</returns>
        bool Matches(string input);

        /// <summary>
        /// Get expanded values from a string
        /// </summary>
        /// <param name="input">String to get from</param>
        /// <returns>A dynamic object holding the values from the string</returns>
        dynamic GetValues(string input);

        /// <summary>
        /// Resolves an input string using the format and mapped format
        /// </summary>
        /// <param name="input">String to resolve</param>
        /// <returns>Resolved string</returns>
        string Resolve(string input);
    }
}
