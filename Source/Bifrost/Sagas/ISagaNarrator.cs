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
using System;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Defines the recorder for <see cref="ISaga"/>
    /// </summary>
    public interface ISagaNarrator
    {
        /// <summary>
        /// Begin a <see cref="ISaga"/>
        /// </summary>
        /// <typeparam name="T">Type of saga to begin</typeparam>
        /// <returns>An instance of the new saga</returns>
        T Begin<T>() where T : ISaga;

        /// <summary>
        /// Continue a <see cref="ISaga"/>
        /// </summary>
        /// <param name="id">Identifier of the saga</param>
        /// <returns>An instance of the <see cref="ISaga"/></returns>
        ISaga Continue(Guid id);

        /// <summary>
        /// Conclude a <see cref="ISaga"/>
        /// </summary>
        /// <param name="saga"></param>
        /// <returns></returns>
        /// <remarks>
        /// Conclusion means that the saga is in fact not available any more
        /// </remarks>
        SagaConclusion Conclude(ISaga saga);

        /// <summary>
        /// Transition to a <see cref="IChapter"/> by type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IChapter"/> to transition to</typeparam>
        /// <param name="saga"><see cref="ISaga"/> to transition</param>
        /// <returns><see cref="ChapterTransition" /> Result of the transition attempt.  If successful, this will contain instance of the target <see cref="IChapter"/> that was transitioned to.  Else, the validation errors.</returns>
        /// <remarks>
        /// If the chapter does not exist it will create it
        /// </remarks>
        ChapterTransition TransitionTo<T>(ISaga saga) where T:IChapter;
    }
}