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
    /// Validates that the chapter is valid and conforms to specified business rules
    /// </summary>
    public interface IChapterValidationService
    {
        /// <summary>
        /// Validate the chapter
        /// </summary>
        /// <param name="chapter">Instance to be validated</param>
        /// <returns>A collection of ValidationResults that indicate any invalid properties / rules</returns>
        IEnumerable<ValidationResult> Validate(IChapter chapter);
    }
}