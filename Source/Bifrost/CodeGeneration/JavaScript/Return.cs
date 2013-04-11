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
namespace Bifrost.CodeGeneration.JavaScript
{
    /// <summary>
    /// Represents a return statement
    /// </summary>
    public class Return : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Literal"/>
        /// </summary>
        /// <param name="returnValue"><see cref="LanguageElement"/> representing the content - the actual literal</param>
        public Return(LanguageElement returnValue)
        {
            ReturnValue = returnValue;
        }

        /// <summary>
        /// Gets or sets the representation of the literal
        /// </summary>
        public LanguageElement ReturnValue { get; set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.WriteWithIndentation("return ");
            ReturnValue.Write(writer);
            writer.Newline();
        }
#pragma warning restore 1591
    }
}
