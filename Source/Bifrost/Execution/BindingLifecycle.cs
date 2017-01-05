/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
	/// <summary>
	/// Scope for activation
	/// </summary>
	public enum BindingLifecycle
	{
		/// <summary>
		/// Scoped as a singleton within the Ninject kernel
		/// </summary>
		Singleton,

		/// <summary>
		/// Scoped as per request - tied into the current WebRequest
		/// </summary>
		Request,

		/// <summary>
		/// Scoped to null
		/// </summary>
		Transient,

		/// <summary>
		/// Scoped to per thread 
		/// </summary>
		Thread
	}
}