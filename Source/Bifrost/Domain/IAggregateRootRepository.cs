/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Domain
{
	/// <summary>
	/// Defines the basic functionality for finding and getting aggregated roots
	/// </summary>
	/// <typeparam name="T">Type of aggregated root</typeparam>
	public interface IAggregateRootRepository<T>
		where T : AggregateRoot
	{
		/// <summary>
		/// Get an aggregated root by id
		/// </summary>
		/// <param name="id">Id of aggregated root to get</param>
		/// <returns>An instance of the aggregated root</returns>
		/// <exception cref="MissingAggregateRootException">Thrown if aggregated root does not exist</exception>
		T Get(Guid id);
	}
}
