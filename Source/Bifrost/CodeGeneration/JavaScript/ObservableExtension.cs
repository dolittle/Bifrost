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

namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents an extension to an <see cref="Observable"/>
    /// </summary>
    /// <remarks>
    /// Extensions augment a Knockout observable with new functionality and has access directly to the observable and its value
    /// </remarks>
    public class ObservableExtension
    {
        /// <summary>
        /// Name of extension
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Options in JSON for the extension that gets passed to the extension during creation
        /// </summary>
        public string Options { get; set; }
    }
}
