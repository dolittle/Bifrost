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

        public TaskContext(object associatedData)
        {
            Result = new TaskResult();
            AssociatedData = associatedData;

            _progress = 0;
        }

        /// <summary>
        /// Gets the result of the task
        /// </summary>
        public TaskResult Result { get; private set; }

        /// <summary>
        /// Gets the associated data
        /// </summary>
        public object AssociatedData { get; private set; }

        double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                PropertyChanged.Notify(() => Progress);
            }
        }

    }
}
