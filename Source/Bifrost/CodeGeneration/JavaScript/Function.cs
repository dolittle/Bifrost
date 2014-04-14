#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
    /// Represents a function
    /// </summary>
    public class Function : LanguageElement
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Function"/>
        /// </summary>
        /// <param name="parameters">Optional parameters for the function</param>
        public Function(params string[] parameters)
        {
            Parameters = parameters;
            Body = new FunctionBody();
        }

        /// <summary>
        /// Gets or sets the parameters for the function
        /// </summary>
        public string[] Parameters { get; set; }

        /// <summary>
        /// Gets the <see cref="FunctionBody"/>
        /// </summary>
        public FunctionBody Body { get; private set; }

#pragma warning disable 1591
        public override void Write(ICodeWriter writer)
        {
            writer.Write("function(");
            for (var parameterIndex = 0; parameterIndex < Parameters.Length; parameterIndex++)
            {
                if (parameterIndex != 0)
                    writer.Write(", ");

                writer.Write(Parameters[parameterIndex]);
            }
            writer.Write(") {{");
            writer.Newline();
            Body.Write(writer);
            writer.WriteWithIndentation("}}");
        }
#pragma warning restore 1591
    }
}
