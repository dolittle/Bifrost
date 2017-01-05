/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Concurrency;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents a <see cref="ITaskScheduler"/>
    /// </summary>
    public class TaskScheduler : ITaskScheduler
    {
        IScheduler _scheduler;
        List<Task> _tasks;
        Dictionary<Task, Guid[]> _taskOperationIds;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskScheduler"/>
        /// </summary>
        /// <param name="scheduler"><see cref="IScheduler"/> used to schedule operations from tasks</param>
        public TaskScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
            _tasks = new List<Task>();
            _taskOperationIds = new Dictionary<Task, Guid[]>();
        }


#pragma warning disable 1591 // Xml Comments
        public void Start(Task task, Action<Task> taskDone=null)
        {
            if (task.IsDone)
            {
                taskDone(task);
                return;
            }

            if (!_tasks.Contains(task))
                _tasks.Add(task);            

            if (task.CanRunOperationsAsynchronously)
                ScheduleOperations(task, taskDone);
            else
                ExecuteOperations(task, taskDone);
        }


        public void Stop(Task task)
        {
            _tasks.Remove(task);
            StopOperationsForTask(task);

        }
#pragma warning restore 1591 // Xml Comments

        void ScheduleOperations(Task task, Action<Task> taskDone)
        {
            _taskOperationIds[task] = new Guid[task.Operations.Length];
            var operationsDone = new bool[task.Operations.Length];
            for (var operationIndex = 0; operationIndex < task.CurrentOperation; operationIndex++)
                operationsDone[operationIndex] = true;

            for (var operationIndex = task.CurrentOperation; operationIndex < task.Operations.Length; operationIndex++)
                ScheduleOperation(task, operationsDone, operationIndex, taskDone);

            if (!_tasks.Contains(task))
                StopOperationsForTask(task);

            if (!operationsDone.Any(b => b == false))
                _taskOperationIds.Remove(task);
        }

        void ScheduleOperation(Task task, bool[] operationsDone, int operationIndex, Action<Task> taskDone)
        {
            var currentOperationIndex = operationIndex;
            _taskOperationIds[task][currentOperationIndex] = _scheduler.Start<Task>(t => t.Operations[currentOperationIndex](t, currentOperationIndex), task, t =>
            {
                task.CurrentOperation = currentOperationIndex + 1;
                operationsDone[currentOperationIndex] = true;

                if( taskDone != null ) 
                    if (!operationsDone.Any(b => b == false))
                        taskDone(task);
            });
        }

        void ExecuteOperations(Task task, Action<Task> taskDone)
        {
            _scheduler.Start<Task>(t =>
            {
                for (var operationIndex = task.CurrentOperation; operationIndex < task.Operations.Length; operationIndex++)
                {
                    task.Operations[operationIndex](task, operationIndex);
                    task.CurrentOperation++;
                    if (!_tasks.Contains(task))
                        break;
                }

                _tasks.Remove(task);
                if( taskDone != null )
                    taskDone(task);
            }, task);
        }

        void StopOperationsForTask(Task task)
        {
            if (_taskOperationIds.ContainsKey(task))
                foreach (var operationId in _taskOperationIds[task])
                    if (operationId != Guid.Empty)
                        _scheduler.Stop(operationId);
        }
    }
}
