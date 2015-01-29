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
using System.IO;
using System.Text;

namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Represents an implementation for <see cref="ICodeGenerator"/>
    /// </summary>
    public class CodeGenerator : ICodeGenerator
    {
#pragma warning disable 1591
        public string GenerateFrom(ILanguageElement languageElement)
        {
            var stringBuilder = new StringBuilder();
            var textWriter = new StringWriter(stringBuilder);
            var writer = new CodeWriter(textWriter);

            languageElement.Write(writer);

            return stringBuilder.ToString();
        }
#pragma warning restore 1591
    }
}
