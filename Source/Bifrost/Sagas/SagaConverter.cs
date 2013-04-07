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
using System.Collections.Generic;
using System.Linq;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Serialization;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a <see cref="ISagaConverter"/>
    /// </summary>
	public class SagaConverter : ISagaConverter
	{
#if(NETFX_CORE)
		static readonly string[] SagaProperties = typeof(ISaga).GetTypeInfo().DeclaredProperties.Select(t => t.Name).ToArray();
#else
		static readonly string[] SagaProperties = typeof(ISaga).GetProperties().Select(t => t.Name).ToArray();
#endif

        static readonly SerializationOptions SerializationOptions =
			new SerializationOptions
				{
					ShouldSerializeProperty = (t, p) =>
					                          	{
					                          		if (typeof (ISaga)
#if(NETFX_CORE)
                                                            .GetTypeInfo().IsAssignableFrom(t.GetTypeInfo())
#else
                                                            .IsAssignableFrom(t)
#endif
                                                        )
					                          			return !SagaProperties.Any(sp => sp == p);

					                          		return true;
					                          	}
				};

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
				saga = (ISaga) _serializer.FromJson(type,sagaHolder.SerializedSaga, SerializationOptions);

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
			sagaHolder.SerializedSaga = _serializer.ToJson(saga, SerializationOptions);
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