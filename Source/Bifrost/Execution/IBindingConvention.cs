/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
	/// <summary>
	/// Defines the basic functionality for a convention that can be applied
	/// to bindings for a <see cref="IContainer"/>
	/// </summary>
	public interface IBindingConvention
	{
		/// <summary>
		/// Checks wether or not a given <see cref="Type">Service</see> can be resolved by the convention
		/// </summary>
        /// <param name="container">Container to resolve binding for</param>
		/// <param name="service">Service that needs to be resolved</param>
		/// <returns>True if it can resolve it, false if not</returns>
		bool CanResolve(IContainer container, Type service);

		/// <summary>
		/// Resolve a <see cref="Type">Service</see>
		/// </summary>
		/// <param name="container">Container to resolve binding for</param>
        /// <param name="service">Service that needs to be resolved</param>
		void Resolve(IContainer container, Type service);
	}
}