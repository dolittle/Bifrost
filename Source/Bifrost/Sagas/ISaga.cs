/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Bifrost.Events;
using System.Reflection;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Defines a saga
    /// </summary>
    public interface ISaga : IEventStore
    {
        /// <summary>
        /// Gets or sets the unique identifier of a saga
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets what partition the saga belongs to
        /// </summary>
        /// <remarks>
        /// Partitions are used to group sagas into logical partitions
        /// </remarks>
        string Partition { get; set; }

        /// <summary>
        /// Gets or sets the unique key within a partition for a saga
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets the chapters in the saga
        /// </summary>
        IEnumerable<IChapter> Chapters { get; }

        /// <summary>
        /// Gets the current chapter in the saga
        /// </summary>
        IChapter CurrentChapter { get; }


        /// <summary>
        /// Set the current chapter
        /// </summary>
        /// <param name="chapter"><see cref="IChapter"/> to set as current</param>
        void SetCurrentChapter(IChapter chapter);

        /// <summary>
        /// Set the current chapter
        /// </summary>
        /// <typeparam name="T"><see cref="IChapter"/> to set as current</typeparam>
        void SetCurrentChapter<T>() where T : IChapter;

        /// <summary>
        /// Add a chapter to the saga
        /// </summary>
        /// <param name="chapter"><see cref="IChapter"/> to add</param>
        void AddChapter(IChapter chapter);

        /// <summary>
        /// Check if the saga contains a chapter based on the type of the chapter
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IChapter"/> to check if saga contains</typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : IChapter;

        /// <summary>
        /// Check if the saga contains a chapter based on the type of the chapter
        /// </summary>
        /// <param name="type">Chapter type</param>
        /// <returns></returns>
        bool Contains(Type type);

        /// <summary>
        /// Get a specific chapter by type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IChapter"/> to get</typeparam>
        /// <returns>Instance of chapter</returns>
        /// <exception cref="ChapterDoesNotExistException">Thrown if chapter not in saga</exception>
        T Get<T>() where T : IChapter;

        /// <summary>
        /// Gets an array of PropertyInfo objects that reflect any Chapter instances that are available
        /// as properties on the Saga
        /// </summary>
        PropertyInfo[] ChapterProperties { get; }

        /// <summary>
        /// Gets and sets the SagaState.  Only to be used directly by serialization.
        /// </summary>
        SagaState CurrentState { get; set; }

        /// <summary>
        /// Indicates whether the saga is continuing
        /// </summary>
        /// <returns></returns>
        bool IsNew { get; }

        /// <summary>
        /// Indicates whether the saga has Begun
        /// </summary>
        /// <returns></returns>
        bool IsBegun { get; }

        /// <summary>
        /// Indicates whether the saga is continuing
        /// </summary>
        /// <returns></returns>
        bool IsContinuing { get; }

        /// <summary>
        /// Indicates whether the saga is concluded
        /// </summary>
        /// <returns></returns>
        bool IsConcluded { get; }

        /// <summary>
        /// Begins the Saga
        /// </summary>
        void Begin();

        /// <summary>
        /// Continues the Saga
        /// </summary>
        void Continue();

        /// <summary>
        /// Concludes the Saga
        /// </summary>
        void Conclude();
        
        /// <summary>
        /// Method that is executed when the Saga is beginning.
        /// </summary>
        void OnBegin();

        /// <summary>
        /// Method that is executed when the Saga is continuing.
        /// </summary>
        void OnContinue();

        /// <summary>
        /// Method that is executed when the Saga is concludiung.
        /// </summary>
        void OnConclude();

        /// <summary>
        /// Get uncommitted events from the <see cref="ISaga"/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<IEvent> GetUncommittedEvents();

        /// <summary>
        /// Set any uncommitted into the <see cref="ISaga"/>
        /// </summary>
        /// <param name="events"></param>
        void SetUncommittedEvents(IEnumerable<IEvent> events);

        /// <summary>
        /// Save any uncommitted events to a given <see cref="IEventStore"/>
        /// </summary>
        /// <param name="eventStore"><see cref="IEventStore"/> to save the events to</param>
        void SaveUncommittedEventsToEventStore(IEventStore eventStore);
    }
}