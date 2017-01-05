/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Views
{
	/// <summary>
	/// Defines the behavior of having an Id - typically used by objects during querying
	/// </summary>
    public interface IHaveId
    {
		/// <summary>
		/// Get the Id of the object
		/// </summary>
        Guid Id { get; }
    }
}
