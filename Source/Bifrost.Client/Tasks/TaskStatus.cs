/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents the status of a <see cref="ITask"/> running in a <see cref="TaskContext">context</see>
    /// </summary>
    public enum TaskStatus
    {
        Idle=1,
        InProgress,
        Failed,
        Succeeded,
        Cancelled
    }
}
