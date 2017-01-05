/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents a <see cref="ITaskManager"/>
    /// </summary>
    public class TaskManager : ITaskManager
    {
        ITaskRepository _taskRepository;
        ITaskScheduler _taskScheduler;
        IContainer _container;
        IEnumerable<ITaskStatusReporter> _reporters;


        /// <summary>
        /// Initializes a new instance of the <see cref="TaskManager"/>
        /// </summary>
        /// <param name="taskRepository">A <see cref="ITaskRepository"/> to load / save <see cref="Task">tasks</see></param>
        /// <param name="taskScheduler">A <see cref="ITaskScheduler"/> for executing tasks and their operations</param>
        /// <param name="typeImporter">A <see cref="ITypeImporter"/> used for importing <see cref="ITaskStatusReporter"/></param>
        /// <param name="container">A <see cref="IContainer"/> to use for getting instances</param>
        public TaskManager(ITaskRepository taskRepository, ITaskScheduler taskScheduler, ITypeImporter typeImporter, IContainer container)
        {
            _taskRepository = taskRepository;
            _taskScheduler = taskScheduler;
            _container = container;
            _reporters = typeImporter.ImportMany<ITaskStatusReporter>();
        }

#pragma warning disable 1591 // Xml Comments
        public T Start<T>() where T : Task
        {
            var task = _container.Get<T>();
            task.Id = Guid.NewGuid();
            task.StateChange += t =>
            {
                _taskRepository.Save(task);
                Report(tt => tt.StateChanged(task));
            };

            _taskRepository.Save(task);

            task.CurrentOperation = 0;
            task.Begin();

            _taskScheduler.Start(task, t => Stop(t.Id));
            Report(t => t.Started(task));

            return task;
        }

        public T Resume<T>(TaskId taskId) where T : Task
        {
            var task = _taskRepository.Load(taskId) as T;
            task.Begin();
            _taskScheduler.Start(task);
            Report(t => t.Resumed(task));
            return task;
        }

        public void Stop(TaskId taskId)
        {
            var task = _taskRepository.Load(taskId);
            task.End();
            _taskRepository.Delete(task);
            Report(t => t.Stopped(task));
        }

        public void Pause(TaskId taskId)
        {
            var task = _taskRepository.Load(taskId);
            _taskScheduler.Stop(task);
            Report(t => t.Paused(task));
        }
#pragma warning restore 1591 // Xml Comments

        void Report(Expression<Action<ITaskStatusReporter>> expression)
        {
            var method = expression.GetMethodInfo();
            var arguments = expression.GetMethodArguments();

            foreach (var reporter in _reporters)
                method.Invoke(reporter, arguments);
        }
    }
}
