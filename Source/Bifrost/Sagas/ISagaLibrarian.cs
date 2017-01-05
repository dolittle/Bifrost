/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Sagas
{
	/// <summary>
	/// Defines a librarian for handling sagas
	/// 
	/// The purpose of the librarian is to persist and get a <see cref="ISaga"/> or multiple
	/// sagas
	/// </summary>
    public interface ISagaLibrarian
    {
		/// <summary>
		/// Close a <see cref="ISaga"/> permanently
		/// </summary>
		/// <param name="saga"><see cref="ISaga"/> to close</param>
    	void Close(ISaga saga);

		/// <summary>
		/// Catalogue saga to the library
		/// </summary>
		/// <param name="saga"><see cref="ISaga"/> to record</param>
        void Catalogue(ISaga saga);

		/// <summary>
		/// Get a <see cref="ISaga"/> based on its id
		/// </summary>
		/// <param name="id">Id of saga to get</param>
		/// <returns>An instance of the <see cref="ISaga"/></returns>
		ISaga Get(Guid id);

		/// <summary>
		/// Get a <see cref="ISaga"/> based on the partition its in and key
		/// </summary>
		/// <param name="partition">Partition identifier</param>
		/// <param name="key">Unique partition key</param>
		/// <returns>An instance of the <see cref="ISaga"/></returns>
		ISaga Get(string partition, string key);

		/// <summary>
		/// Get all sagas within a given partition
		/// </summary>
		/// <param name="partition">Partition identifier</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ISaga"/> with all sagas in the given partition</returns>
    	IEnumerable<ISaga> GetForPartition(string partition);
    }
}