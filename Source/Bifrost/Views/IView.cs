/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;

namespace Bifrost.Views
{
	/// <summary>
	/// Defines a repository that can be queried against
	/// </summary>
	/// <typeparam name="T">Type that can be queried against</typeparam>
	public interface IView<T>
	{
		/// <summary>
		/// Gets a queryable that can be queried against
		/// </summary>
		IQueryable<T> Query { get; }

        /// <summary>
        /// Gets a single instance based on Id
        /// </summary>
        /// <param name="id">Id of instance to get</param>
        /// <returns>The instance found - null if not found</returns>
	    T GetById(Guid id);
	}
}
