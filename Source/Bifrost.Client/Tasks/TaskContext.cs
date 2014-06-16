#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System.ComponentModel;
using Bifrost.Values;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents the context used when executing <see cref="ITask">tasks</see>
    /// </summary>
    public class TaskContext : INotifyPropertyChanged
    {
#pragma warning disable 1591 // Xml Comments
        public event PropertyChangedEventHandler PropertyChanged = (s,e) => {};
#pragma warning restore 1591 // Xml Comments

        event TaskCompleted _completed = (c) => { };
        event TaskSucceeded _succeeded = (c) => { };
        event TaskFailed _failed = (c) => { };
        event TaskCancelled _cancelled = (c) => { };

        /// <summary>
        /// Initializes the <see cref="TaskContext"/>
        /// </summary>
        /// <param name="associatedData"></param>
        public TaskContext(ITask task, object associatedData)
        {
            Task = task;
            Result = new TaskResult();
            AssociatedData = associatedData;

            _progress = 0;
        }

        /// <summary>
        /// The <see cref="ITask"/> that is related to the context
        /// </summary>
        public ITask Task { get; private set; }

        /// <summary>
        /// <see cref="TaskStatus"/> of the context
        /// </summary>
        public TaskStatus Status { get; private set; }

        /// <summary>
        /// Gets the result of the task
        /// </summary>
        public TaskResult Result { get; private set; }

        /// <summary>
        /// Gets the associated data
        /// </summary>
        public object AssociatedData { get; private set; }

        double _progress;

        /// <summary>
        /// Gets or sets the progress of the task
        /// </summary>
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                PropertyChanged.Notify(() => Progress);
            }
        }

        /// <summary>
        /// Fail the context
        /// </summary>
        /// <remarks>
        /// It will also complete the context afterwards
        /// </remarks>
        public void Fail()
        {
            Status = TaskStatus.Failed;
            _failed(this);
            _completed(this);
        }

        /// <summary>
        /// Succeed the context
        /// </summary>
        /// <remarks>
        /// It will also complete the context afterwards
        /// </remarks>
        public void Succeed()
        {
            Status = TaskStatus.Succeeded;
            _succeeded(this);
            _completed(this);
        }

        /// <summary>
        /// Cancel the context
        /// </summary>
        /// <remarks>
        /// It will also complete the context afterwards
        /// </remarks>
        public void Cancel()
        {
            Status = TaskStatus.Cancelled;
            _cancelled(this);
            _completed(this);
        }


        /// <summary>
        /// Add a <see cref="TaskCompleted"/> callback
        /// </summary>
        /// <param name="callback"><see cref="TaskCompleted"/> callback to add</param>
        /// <returns>The <see cref="TaskContext"/></returns>
        public TaskContext Completed(TaskCompleted callback)
        {
            if (IsComplete) callback(this);

            _completed += callback;
            return this;
        }

        /// <summary>
        /// Add a <see cref="TaskSucceeded"/> callback
        /// </summary>
        /// <param name="callback"><see cref="TaskSucceeded"/> callback to add</param>
        /// <returns>The <see cref="TaskContext"/></returns>
        public TaskContext Succeeded(TaskSucceeded callback)
        {
            if (HasSucceeded) callback(this);

            _succeeded += callback;
            return this;
        }

        /// <summary>
        /// Add a <see cref="TaskFailed"/> callback
        /// </summary>
        /// <param name="callback"><see cref="TaskFailed"/> callback to add</param>
        /// <returns>The <see cref="TaskContext"/></returns>
        public TaskContext Failed(TaskFailed callback)
        {
            if (HasFailed) callback(this);

            _failed += callback;
            return this;
        }

        /// <summary>
        /// Add a <see cref="TaskCancelled"/> callback
        /// </summary>
        /// <param name="callback"><see cref="TaskCancelled"/> callback to add</param>
        /// <returns>The <see cref="TaskContext"/></returns>
        public TaskContext Cancelled(TaskCancelled callback)
        {
            if (IsCancelled) callback(this);

            _cancelled += callback;
            return this;
        }

        /// <summary>
        /// Gets wether or not the context was cancelled
        /// </summary>
        public bool IsCancelled
        {
            get
            {
                return Status == TaskStatus.Cancelled;
            }
        }

        /// <summary>
        /// Gets wether or not the context is complete
        /// </summary>
        public bool IsComplete
        {
            get
            {
                return Status == TaskStatus.Cancelled ||
                       Status == TaskStatus.Failed ||
                       Status == TaskStatus.Succeeded;
            }
        }

        /// <summary>
        /// Gets wether or not the context has succeeded
        /// </summary>
        public bool HasSucceeded
        {
            get
            {
                return Status == TaskStatus.Succeeded;
            }
        }

        /// <summary>
        /// Gets wether or not the context is failed
        /// </summary>
        public bool HasFailed
        {
            get
            {
                return Status == TaskStatus.Failed;
            }
        }

    }
}
