/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines the store for getting and saving <see cref="CommandContext">CommandContext</see> from its persistent data store
    /// </summary>
	public interface ICommandContextStore
	{
        /// <summary>
        /// Get a specific <see cref="CommandContext">CommandContext</see> based on its Id
        /// </summary>
        /// <param name="id">The Id of the <see cref="CommandContext">CommandContext</see> to get</param>
        /// <returns>The actual <see cref="CommandContext">CommandContext</see></returns>
		CommandContext Get(Guid id);
	}
}
