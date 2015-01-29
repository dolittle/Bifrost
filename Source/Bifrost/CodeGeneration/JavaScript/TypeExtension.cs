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
    /// Represents a Bifrost specific Type extension
    /// </summary>
    public class TypeExtension : Container
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TypeExtension"/>
        /// </summary>
        /// <param name="superType">Super type that is being extended, default is "Bifrost.Type"</param>
        public TypeExtension(string superType="Bifrost.Type")
        {
            SuperType = superType;
            Function = new Function();
        }

        /// <summary>
        /// Gets or sets the type being extended
        /// </summary>
        public string SuperType { get; set; }

        /// <summary>
        /// Gets the function representing the type
        /// </summary>
        public Function Function { get; private set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write("{0}.extend(", SuperType);
            Function.Write(writer);
            writer.Write(")");
        }
#pragma warning restore 1591
    }
}
