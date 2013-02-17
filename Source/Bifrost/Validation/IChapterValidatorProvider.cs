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
using System;
using Bifrost.Sagas;

namespace Bifrost.Validation
{
    /// <summary>
    /// Defines a provider that returns chapter-specific validators
    /// </summary>
    public interface IChapterValidatorProvider
    {
        /// <summary>
        /// Retrieves an validator specific to the chapter
        /// </summary>
        /// <param name="chapter">Chapter to be validates</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available</returns>
        ICanValidate GetValidatorFor(IChapter chapter);

        /// <summary>
        /// Retrieves an validator specific to the chapter type
        /// </summary>
        /// <param name="type">Type of the Chapter to be validates</param>
        /// <returns>Returns specific validator or a NullValidator if no validator is available</returns>
        ICanValidate GetValidatorFor(Type type);
    }
}