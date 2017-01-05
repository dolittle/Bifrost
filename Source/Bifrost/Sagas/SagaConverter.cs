/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Serialization;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a <see cref="ISagaConverter"/>
    /// </summary>
	public class SagaConverter : ISagaConverter
	{
        private static readonly ISerializationOptions SagaSerializationOptions = new SagaSerializationOptions();

        readonly IContainer _container;
		readonly ISerializer _serializer;

        /// <summary>
        /// Initializes a new instance of <see cref="SagaConverter"/>
        /// </summary>
        /// <param name="container">A <see cref="IContainer"/> for creating instances of types</param>
        /// <param name="serializer">A <see cref="ISerializer"/> to use for serialization</param>
		public SagaConverter(IContainer container, ISerializer serializer)
		{
			_container = container;
			_serializer = serializer;
		}

#pragma warning disable 1591 // Xml Commentsz
        public ISaga ToSaga(SagaHolder sagaHolder)
		{
			if (string.IsNullOrEmpty(sagaHolder.Type))
				return null;

			var type = Type.GetType(sagaHolder.Type);

			Type currentChapterType = null;
			if (!string.IsNullOrEmpty(sagaHolder.CurrentChapterType))
				currentChapterType = Type.GetType(sagaHolder.CurrentChapterType);

			ISaga saga;
			if (string.IsNullOrEmpty(sagaHolder.SerializedSaga))
				saga = _container.Get(type) as ISaga;
			else
				saga = (ISaga) _serializer.FromJson(type,sagaHolder.SerializedSaga, SagaSerializationOptions);

			saga.Id = sagaHolder.Id;
			saga.Partition = sagaHolder.Partition;
			saga.Key = sagaHolder.Key;
			saga.CurrentState = sagaHolder.State;

			if (!string.IsNullOrEmpty(sagaHolder.UncommittedEvents))
			{
				var uncommittedEvents = new List<IEvent>();
				_serializer.FromJson(uncommittedEvents,sagaHolder.UncommittedEvents);
				saga.SetUncommittedEvents(uncommittedEvents);
			}

			DeserializeChapters(sagaHolder, saga, currentChapterType);
			

			return saga;
		}

		public SagaHolder ToSagaHolder(ISaga saga)
		{
			var sagaHolder = new SagaHolder();
			Populate(sagaHolder, saga);
			return sagaHolder;
		}

		public void Populate(SagaHolder sagaHolder, ISaga saga)
		{
			sagaHolder.Id = saga.Id;
			sagaHolder.Name = saga.GetType().Name;
			sagaHolder.Type = saga.GetType().AssemblyQualifiedName;
			sagaHolder.Key = saga.Key;
			sagaHolder.Partition = saga.Partition;
			sagaHolder.State = saga.CurrentState.ToString();
			sagaHolder.SerializedSaga = _serializer.ToJson(saga, SagaSerializationOptions);
			sagaHolder.UncommittedEvents = _serializer.ToJson(saga.GetUncommittedEvents());

			var chapterHolders = (from c in saga.Chapters
			                      select GetChapterHolderFromChapter(c)).ToArray();

			sagaHolder.SerializedChapters = _serializer.ToJson(chapterHolders);

			if (saga.CurrentChapter != null)
				sagaHolder.CurrentChapterType = saga.CurrentChapter.GetType().AssemblyQualifiedName;
		}
#pragma warning restore 1591 // Xml Comments



        ChapterHolder GetChapterHolderFromChapter(IChapter chapter)
		{
			var chapterHolder = new ChapterHolder
			                    	{
			                    		Type = chapter.GetType().AssemblyQualifiedName,
			                    		SerializedChapter = _serializer.ToJson(chapter)
			                    	};
			return chapterHolder;
		}

		void DeserializeChapters(SagaHolder sagaHolder, ISaga saga, Type currentChapterType)
		{
			if (!string.IsNullOrEmpty(sagaHolder.SerializedChapters))
			{
				var chapterHolders = new List<ChapterHolder>();
				_serializer.FromJson(chapterHolders,sagaHolder.SerializedChapters);
				foreach (var chapterHolder in chapterHolders)
				{
					var chapterType = Type.GetType(chapterHolder.Type);
					var chapter = _container.Get(chapterType) as IChapter;

					if (!string.IsNullOrEmpty(chapterHolder.SerializedChapter))
						_serializer.FromJson(chapter, chapterHolder.SerializedChapter);

					saga.AddChapter(chapter);

					if (currentChapterType != null &&
						chapterType == currentChapterType)
						saga.SetCurrentChapter(chapter);
				}
			}
		}
	}
}