/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{
	/// <summary>
	/// Defines a manager for binding conventions
	/// </summary>
    public interface IBindingConventionManager
    {
		/// <summary>
		/// Add a convention by type
		/// </summary>
		/// <param name="type">Type of convention to add</param>
		/// <remarks>
		/// The type must implement the <see cref="IBindingConvention"/>
		/// </remarks>
        void Add(Type type);

		/// <summary>
		/// Add a convention by type generically
		/// </summary>
		/// <typeparam name="T">Type of convention to add</typeparam>
        void Add<T>() where T : IBindingConvention;

		/// <summary>
		/// Initialize system
		/// </summary>
        void Initialize();

		/// <summary>
		/// Discover bindings and initialize
		/// </summary>
        void DiscoverAndInitialize();
    }
}
