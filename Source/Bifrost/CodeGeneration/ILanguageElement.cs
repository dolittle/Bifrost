#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System.IO;

namespace Bifrost.CodeGeneration
{
    /// <summary>
    /// Defines the contract of a language element
    /// </summary>
    public interface ILanguageElement
    {
        /// <summary>
        /// Gets or sets the parent <see cref="ILanguageElement"/>
        /// </summary>
        ILanguageElement Parent { get; set; }

        /// <summary>
        /// Add a child <see cref="ILanguageElement"/> to this
        /// </summary>
        /// <param name="element"><see cref="ILanguageElement"/> to add</param>
        void AddChild(ILanguageElement element);

        /// <summary>
        /// Writes code to the <see cref="ICodeWriter"/>
        /// </summary>
        /// <param name="writer"><see cref="ICodeWriter"/> to write to</param>
        void Write(ICodeWriter writer);
    }
}
