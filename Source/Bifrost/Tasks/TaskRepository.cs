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
using Bifrost.Entities;
using Bifrost.Execution;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents a <see cref="ITaskRepository"/>
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        IEntityContext<TaskEntity> _entityContext;
        IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskRepository"/>
        /// </summary>
        /// <param name="entityContext"><see cref="IEntityContext{T}"/> that is used for persisting <see cref="Task">tasks</see></param>
        /// <param name="container"><see cref="IContainer"/> to use for creating instances of <see cref="Task">tasks</see></param>
        public TaskRepository(IEntityContext<TaskEntity> entityContext, IContainer container)
        {
            _entityContext = entityContext;
            _container = container;
        }


#pragma warning disable 1591 // Xml Comments
        public IEnumerable<Task> LoadAll()
        {
            return _entityContext.Entities.Select(ToTask);
        }

        public Task Load(TaskId taskId)
        {
            return _entityContext.Entities.Where(e => e.Id == taskId.Value).Select(ToTask).Single();
        }

        public void Save(Task task)
        {
            var existing = _entityContext.Entities.SingleOrDefault(t => t.Id == task.Id);
            if ( existing != null )
                _entityContext.Update(ToTaskEntity(task, existing));
            else
                _entityContext.Insert(ToTaskEntity(task));
        }

        public void Delete(Task task)
        {
            _entityContext.Delete(new TaskEntity { Id = task.Id });
        }

        public void DeleteById(TaskId taskId)
        {
            _entityContext.DeleteById(taskId.Value);
        }
#pragma warning restore 1591 // Xml Comments


        Task ToTask(TaskEntity taskEntity)
        {
            var task = _container.Get(taskEntity.Type) as Task;
            task.Id = taskEntity.Id;
            task.CurrentOperation = taskEntity.CurrentOperation;
            PopulatePropertiesFromState(task, taskEntity);
            return task;
        }

        void PopulatePropertiesFromState(Task target, TaskEntity source)
        {
            var targetType = target.GetType();
            foreach (var key in source.State.Keys)
            {
                var property = targetType.GetProperty(key);
                if (property != null)
                {
                    var value = Convert.ChangeType(source.State[key], property.PropertyType);
                    property.SetValue(target, value, null);
                }
            }
        }

        TaskEntity ToTaskEntity(Task task, TaskEntity taskEntity = null)
        {
            if( taskEntity == null ) 
                taskEntity = new TaskEntity();

            taskEntity.Id = task.Id;
            taskEntity.CurrentOperation = task.CurrentOperation;
            taskEntity.Type = task.GetType();
            PopulateStateFromProperties(taskEntity, task);
            return taskEntity;
        }

        void PopulateStateFromProperties(TaskEntity target, Task source)
        {
            var sourceType = source.GetType();
            var taskType = typeof(Task);
            var taskProperties = taskType.GetProperties();
            var sourceProperties = sourceType.GetProperties().Where(p => p.DeclaringType == sourceType && !taskProperties.Any(pp=>pp.Name == p.Name) );
            target.State = sourceProperties.ToDictionary(p=>p.Name, p=>p.GetValue(source, null).ToString());
        }
    }
}
