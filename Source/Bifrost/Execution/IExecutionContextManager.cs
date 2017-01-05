/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a manager for managing <see cref="IExecutionContext">ExecutionContexts</see>
    /// </summary>
    public interface IExecutionContextManager
    {
        /// <summary>
        /// Get the current <see cref="IExecutionContext"/>
        /// </summary>
        IExecutionContext Current { get; }
    }
}
