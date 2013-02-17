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

namespace Bifrost.Sagas
{
	/// <summary>
	/// The exception that is thrown if a transition between two chapters are not allowed
	/// </summary>
    public class ChapterTransitionNotAllowedException : Exception
    {
		/// <summary>
		/// Initializes an instance of <see cref="ChapterTransitionNotAllowedException"/>
		/// </summary>
		/// <param name="from">From <see cref="IChapter"/></param>
		/// <param name="to">To <see cref="IChapter"/></param>
        public ChapterTransitionNotAllowedException(Type from, Type to) : base(string.Format("Can't transition from {0} to {1} - not allowed", from.Name, to.Name)) {}
    }
}