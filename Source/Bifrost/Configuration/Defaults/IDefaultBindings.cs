/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Execution;

namespace Bifrost.Configuration.Defaults
{
	/// <summary>
	/// Defines a system that sets up default bindings
	/// </summary>
    public interface IDefaultBindings
    {
		/// <summary>
		/// Initialize the bindings with the given container
		/// </summary>
		/// <param name="container">The <see cref="IContainer"/> to define the bindings with</param>
        void Initialize(IContainer container);
    }
}