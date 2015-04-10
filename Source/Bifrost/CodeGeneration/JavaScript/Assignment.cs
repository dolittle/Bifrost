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
    /// Represents an assignment - a base for any assignments
    /// </summary>
    public abstract class Assignment : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Assignment"/>
        /// </summary>
        /// <param name="name">Name of the assignment</param>
        /// <param name="value"><see cref="ILanguageElement">Value</see> to assign</param>
        public Assignment(string name, ILanguageElement value = null)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the name of the assignment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ILanguageElement">value</see> to assign
        /// </summary>
        public ILanguageElement Value { get; set; }

#pragma warning disable 1591
        public abstract override void Write(ICodeWriter writer);
#pragma warning restore 1591
    }
}
