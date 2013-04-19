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
using System.Linq;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Sagas
{
	/// <summary>
	/// Provides helper methods for chapters and transitions
	/// </summary>
    public static class ChapterTransitionHelper
    {
		/// <summary>
		/// Check if a transition is allowed between to chapters by type
		/// </summary>
		/// <typeparam name="TF">From <see cref="IChapter"/></typeparam>
		/// <typeparam name="TT">To <see cref="IChapter"/></typeparam>
		/// <returns>True if transition is allowed, false if not</returns>
        public static bool CanTransition<TF, TT>()
            where TF : IChapter
            where TT : IChapter
        {
            return CanTransition(typeof (TF), typeof (TT));
        }

		/// <summary>
		/// Check if a transition is allowed between to chapters by instance of chapters
		/// </summary>
		/// <param name="fromChapter">From <see cref="IChapter"/></param>
		/// <param name="toChapter">To <see cref="IChapter"/></param>
		/// <returns>True if transition is allowed, false if not</returns>
		public static bool CanTransition(IChapter fromChapter, IChapter toChapter)
        {
            return CanTransition(fromChapter.GetType(), toChapter.GetType());
        }

		/// <summary>
		/// Check if a transition is allowed between to chapters by type
		/// </summary>
		/// <param name="fromChapterType">From <see cref="IChapter"/></param>
		/// <param name="toChapterType">To <see cref="IChapter"/></param>
		/// <returns>True if transition is allowed, false if not</returns>
		public static bool CanTransition(Type fromChapterType, Type toChapterType)
        {
            var targetTransitionType = typeof (ICanTransitionTo<>).MakeGenericType(toChapterType);
            return fromChapterType
#if(NETFX_CORE)
                .GetTypeInfo().ImplementedInterfaces
#else
                .GetInterfaces()
#endif
                .Where(t => t == targetTransitionType).SingleOrDefault() != null;
        }
    }
}