/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;

namespace Bifrost.Entities
{
	/// <summary>
	/// Defines a context for working with entities typically stored in a datastore
	/// </summary>
	/// <typeparam name="T">Type of entity the context works on</typeparam>
	public interface IEntityContext<T> : IDisposable
	{
		/// <summary>
		/// Gets a queryable that one can do queries against
		/// </summary>  
		IQueryable<T> Entities { get; }

        //T GetBy<TProperty>(Expression<Func<T, TProperty>> property, TProperty value);

		/// <summary>
		/// Attach an entity to the context
		/// </summary>
		/// <param name="entity">Entity to attach</param>
		/// <remarks>
		/// In some conditions you might have an untracked entity, in order for that
		/// entity to be state handled by some implementations; you need to attach it.
		/// </remarks>
		void Attach(T entity);

		/// <summary>
		/// Insert a newly created entity
		/// </summary>
		/// <param name="entity">Entity to insert</param>
		void Insert(T entity);

		/// <summary>
		/// Update an existing entity
		/// </summary>
		/// <param name="entity">Entity to update</param>
		void Update(T entity);

		/// <summary>
		/// Delete an existing entity
		/// </summary>
		/// <param name="entity">Entity to delete</param>
		void Delete(T entity);

        /// <summary>
        /// Save en existing entity
        /// </summary>
        /// <param name="entity">Entity to save</param>
	    void Save(T entity);

		/// <summary>
		/// Commit any changes in the context
		/// </summary>
		void Commit();

        /// <summary>
        /// Get an entity by its id
        /// </summary>
        /// <typeparam name="TProperty">Property to compare</typeparam>
        /// <param name="id">Id to lookup</param>
        /// <returns></returns>
        T GetById<TProperty>(TProperty id);

        /// <summary>
        /// Delete an entity by its id 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="id"></param>
        void DeleteById<TProperty>(TProperty id);
    }
}
