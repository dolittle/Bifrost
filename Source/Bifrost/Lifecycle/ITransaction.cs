/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Lifecycle
{
	/// <summary>
	/// Defines a logical transaction
	/// </summary>
	public interface ITransaction : IDisposable
	{
		/// <summary>
		/// Commits the transaction
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollback to the state before the transaction started
		/// </summary>
		void Rollback();
	}
}
