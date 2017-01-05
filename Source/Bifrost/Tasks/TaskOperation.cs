/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents the method that gets called to handle a operation within a <see cref="Task"/>
    /// </summary>
    /// <param name="task"><see cref="Task"/> that owns the operation</param>
    /// <param name="operationIndex">The index of the operation within its declaring task</param>
    public delegate void TaskOperation(Task task, int operationIndex);
}
