/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.Reflection;

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
                .GetTypeInfo().ImplementedInterfaces
                .Where(t => t == targetTransitionType).SingleOrDefault() != null;
        }
    }
}