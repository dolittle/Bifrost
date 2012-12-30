#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
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
        public void Start(Task task)
        {
            if (task.IsDone)
                return;

            if (!_tasks.Contains(task))
                _tasks.Add(task);            

            if (task.CanRunOperationsAsynchronously)
                ScheduleOperations(task);
            else
                ExecuteOperations(task);
        }


        public void Stop(Task task)
        {
            _tasks.Remove(task);
            StopOperationsForTask(task);

        }
#pragma warning restore 1591 // Xml Comments

        void ScheduleOperations(Task task)
        {
            _taskOperationIds[task] = new Guid[task.Operations.Length];
            var operationsDone = new bool[task.Operations.Length];

            for (var operationIndex = task.CurrentOperation; operationIndex < task.Operations.Length; operationIndex++)
            {
                _taskOperationIds[task][operationIndex] = _scheduler.Start<Task>(t => t.Operations[operationIndex](t, operationIndex), task, t =>
                {
                    task.CurrentOperation = operationIndex + 1;
                    operationsDone[operationIndex] = true;
                });
            }

            if (!_tasks.Contains(task))
                StopOperationsForTask(task);

            if (!operationsDone.Any(b => b == false))
                _taskOperationIds.Remove(task);
        }

        void ExecuteOperations(Task task)
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
