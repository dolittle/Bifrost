/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Events;

namespace Bifrost.Domain
{
	/// <summary>
	/// Represents the base class used for aggregated roots in your domain
	/// </summary>
	public class AggregateRoot : EventSource, IAggregateRoot
	{
		/// <summary>
		/// Initializes a new instance of an <see cref="AggregateRoot">AggregatedRoot</see>
		/// </summary>
		/// <param name="id">Id of the AggregatedRoot</param>
	    protected AggregateRoot(Guid id) : base(id)
	    {}
	}
}
