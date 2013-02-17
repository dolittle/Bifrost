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
namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Defines a writer for writing code to, typically used by <see cref="ILanguageElement">language elements</see>
    /// </summary>
    public interface ICodeWriter
    {
        /// <summary>
        /// Increase indentation
        /// </summary>
        void Indent();

        /// <summary>
        /// Decrease indentation
        /// </summary>
        void Unindent();

        /// <summary>
        /// Write string with indentation applied
        /// </summary>
        /// <param name="format"><see cref="string"/> format</param>
        /// <param name="args">Args used by the format string</param>
        void WriteWithIndentation(string format, params object[] args);

        /// <summary>
        /// Write string without indentation applied
        /// </summary>
        /// <param name="format"><see cref="string"/> format</param>
        /// <param name="args">Args used by the format string</param>
        void Write(string format, params object[] args);

        /// <summary>
        /// Add a newline 
        /// </summary>
        void Newline();
    }
}
