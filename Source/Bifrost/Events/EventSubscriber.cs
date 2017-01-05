/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Bifrost.Configuration;
using Bifrost.Entities;
using Bifrost.Views;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a base class implementation of an <see cref="IProcessEvents"/>
    /// </summary>
    /// <typeparam name="T">Type of view object the subscriber is working on</typeparam>
    public class EventSubscriber<T> : IProcessEvents
        where T : IHaveId, new()
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EventSubscriber{T}"/>
        /// </summary>
        public EventSubscriber()
        {
            Repository = Configure.Instance.Container.Get<IView<T>>();
            EntityContext = Configure.Instance.Container.Get<IEntityContext<T>>();
        }

        /// <summary>
        /// Gets the <see cref="IView{T}"/> for the type for querying
        /// </summary>
        protected IView<T> Repository { get; private set; }

        /// <summary>
        /// Gets the full <see cref="IEntityContext{T}"/> for CRUD operations for the type
        /// </summary>
        protected IEntityContext<T> EntityContext { get; private set; }


        /// <summary>
        /// Insert an entity into the datasource
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        protected void InsertEntity(T entity)
        {
            EntityContext.Insert(entity);
            EntityContext.Commit();
        }

        /// <summary>
        /// Delete an entity based upon an incoming <see cref="IEvent"/>
        /// </summary>
        /// <param name="event">Event to delete based upon</param>
        protected void DeleteEntity(IEvent @event)
        {
            var entity = GetEntity(@event.EventSourceId);
            DeleteEntity(entity);
        }

        /// <summary>
        /// Delete a specific entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        protected void DeleteEntity(T entity)
        {
            EntityContext.Delete(entity);
            EntityContext.Commit();
        }

        /// <summary>
        /// Check if an entity already exists
        /// </summary>
        /// <param name="entity">Entity to check if exists</param>
        /// <returns>True if exists, false if not</returns>
        protected bool Exists(T entity)
        {
            return GetEntity(entity.Id)==null?false:true;
        }


        /// <summary>
        /// Saves an entity - if it doesn't already exist, it inserts else it updates it
        /// </summary>
        /// <param name="entity">Entity to save</param>
        protected void SaveEntity(T entity)
        {
            if (!Exists(entity))
                InsertEntity(entity);
            else
                EntityContext.Update(entity);

            EntityContext.Commit();
        }


        /// <summary>
        /// Get an entity based upon its ID (<see cref="Guid"/>)
        /// </summary>
        /// <param name="id">Id of the entity to get</param>
        /// <returns>An instance of the entity</returns>
        protected T GetEntity(Guid id)
        {
            var tag = (from p in EntityContext.Entities
                       where p.Id == id
                       select p).Single();
            return tag;
        }


        /// <summary>
        /// Update a property on an entity and commit the change
        /// </summary>
        /// <param name="event"><see cref="IEvent"/> event that will be used to get the entity</param>
        /// <param name="propertyAction">Action to call that will do the changes it needs</param>
        /// <remarks>
        /// This method gets the entity, calls the action provided that will modify one or more properties
        /// and then updates the entity and commits it to the datasource
        /// </remarks>
        protected void UpdateProperty(IEvent @event, Action<T> propertyAction)
        {
            var entity = GetEntity(@event.EventSourceId);
            propertyAction(entity);
            EntityContext.Update(entity);
            EntityContext.Commit();
        }
    }
}
