/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Entities
{
	/// <summary>
	/// Marker interface for connection configuration for <see cref="IEntityContext{T}">entity contexts</see>
	/// </summary>
	public interface IEntityContextConnection
	{
        /// <summary>
        /// Initialize the specific EntityContextConnection
        /// </summary>
        /// <param name="container">Container</param>
        void Initialize(IContainer container);
	}
}
