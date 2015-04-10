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
using System.Linq;
using Bifrost.Events;
using Bifrost.Execution;
using Bifrost.Validation;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Represents a <see cref="ISagaNarrator"/>
    /// </summary>
    public class SagaNarrator : ISagaNarrator
    {
        readonly ISagaLibrarian _librarian;
        readonly IContainer _container;
        readonly IChapterValidationService _chapterValidationService;
        readonly IEventStore _eventStore;

        /// <summary>
        /// Initializes a new instance of <see cref="SagaNarrator"/>
        /// </summary>
        /// <param name="librarian"><see cref="ISagaLibrarian"/> to use for handling sagas</param>
        /// <param name="container"><see cref="IContainer"/> for creating instances</param>
        /// <param name="chapterValidationService"><see cref="IChapterValidationService" /> for validating chapters</param>
        /// <param name="eventStore"></param>
        public SagaNarrator(
            ISagaLibrarian librarian,
            IContainer container,
            IChapterValidationService chapterValidationService,
            IEventStore eventStore)
        {
            _librarian = librarian;
            _container = container;
            _chapterValidationService = chapterValidationService;
            _eventStore = eventStore;
        }

#pragma warning disable 1591 // Xml Comments
        public T Begin<T>() where T : ISaga
        {
            var saga = _container.Get<T>();
            CreateChaptersFromPropertiesOnSaga(saga);
            saga.Begin();
            return saga;
        }

        public ISaga Continue(Guid id)
        {
            var saga = _librarian.Get(id);
            saga.Continue();
            return saga;
        }

        public SagaConclusion Conclude(ISaga saga)
        {
            var conclusion = new SagaConclusion();
            try
            {
                saga.Conclude();
                saga.SaveUncommittedEventsToEventStore(_eventStore);
                _librarian.Close(saga);
            } catch( Exception ex)
            {
                conclusion.Exception = ex;
            }

            return conclusion;
        }

        public ChapterTransition TransitionTo<T>(ISaga saga) where T : IChapter
        {
            var chapterTransition = new ChapterTransition();
            if (saga.CurrentChapter != null && saga.CurrentChapter.GetType().Equals(typeof(T)))
            {
                saga.CurrentChapter.OnTransitionedTo();
                chapterTransition.TransitionedTo = (T)saga.CurrentChapter;
                return chapterTransition;
            }

            chapterTransition.ValidationResults = _chapterValidationService.Validate(saga.CurrentChapter);

            if (chapterTransition.Invalid)
                return chapterTransition;

            ThrowIfTransitionNotAllowed(saga, typeof(T));
            var chapter = GetExistingChapterIfAnyFrom(saga, typeof(T));
            if (chapter == null)
            {
                chapter = _container.Get<T>();
                saga.AddChapter(chapter);
            }

            saga.SetCurrentChapter(chapter);
            chapter.OnTransitionedTo();
            _librarian.Catalogue(saga);

            chapterTransition.TransitionedTo = (T)chapter;
            return chapterTransition;
        }

#pragma warning restore 1591 // Xml Comments

        void CreateChaptersFromPropertiesOnSaga(ISaga saga)
        {
            foreach( var chapterProperty in saga.ChapterProperties )
            {
                var chapter = _container.Get(chapterProperty.PropertyType) as IChapter;
                chapter.OnCreated();
                saga.AddChapter(chapter);
            }
        }

        static IChapter GetExistingChapterIfAnyFrom(ISaga saga, Type type)
        {
            var chapter = saga.Chapters.Where(s => s.GetType() == type).SingleOrDefault();
            return chapter;
        }

        static void ThrowIfTransitionNotAllowed(ISaga saga, Type to)
        {
            if( saga.CurrentChapter != null && !ChapterTransitionHelper.CanTransition(saga.CurrentChapter.GetType(), to))
                throw new ChapterTransitionNotAllowedException(saga.CurrentChapter.GetType(), to);
        }

        static void ThrowIfChapterAlreadyExist(ISaga saga, IChapter chapter)
        {
            if( saga.Chapters.Any(s => s.GetType() == chapter.GetType()) )
                throw new ChapterAlreadyExistException();
        }
    }
}