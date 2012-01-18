#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Sagas;

namespace Bifrost.Validation
{
    /// <summary>
    /// Defines a validator for a Saga <see href="IChapter">Chapter</see>
    /// </summary>
    public interface IChapterValidator
    {
        /// <summary>
        /// Validates that a chapter has all the requirements to allow transition.
        /// </summary>
        /// <param name="chapter">The chapter to validate</param>
        /// <returns>A collection of ValidationResults.  An empty collection indicates a valid chapter.</returns>
        IEnumerable<ValidationResult> ValidateChapter(IChapter chapter);
    }
}