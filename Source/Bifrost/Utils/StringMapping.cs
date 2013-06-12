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
    /// Represents a mapping for a string
    /// </summary>
    public class StringMapping
    {
        /// <summary>
        /// Initializes a new instance of <see cref="StringMapping"/>
        /// </summary>
        /// <param name="format">Originating format</param>
        /// <param name="mappedFormat">Mapped format</param>
        public StringMapping(string format, string mappedFormat)
        {
            Format = format;
            MappedFormat = mappedFormat;
        }

#pragma warning disable 1591 // Xml Comments
        public string Format { get; private set; }
        public string MappedFormat { get; private set; }
#pragma warning restore 1591 // Xml Comments
    }
}
