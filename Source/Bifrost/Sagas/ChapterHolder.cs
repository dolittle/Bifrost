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
namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a holder for a <see cref="IChapter"/>, typically used for serializing a chapter
    /// </summary>
	public class ChapterHolder
	{
        /// <summary>
        /// Gets or sets the type of the chapter
        /// </summary>
        /// <remarks>
        /// Fully Assembly qualified name
        /// </remarks>
		public string Type { get; set; }

        /// <summary>
        /// Gets or sets the serialized version of the <see cref="IChapter"/>
        /// </summary>
		public string SerializedChapter { get; set; }
	}
}