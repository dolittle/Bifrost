/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Delegate that gets called when a task is failed
    /// </summary>
    /// <param name="context">Context of the task</param>
    public delegate void TaskFailed(TaskContext context);
}
