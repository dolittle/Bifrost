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
using System.Collections.Generic;
using System.ComponentModel;
using Bifrost.Values;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents an implementation of <see cref="ITasks"/>
    /// </summary>
    public class Tasks : ITasks, INotifyPropertyChanged
    {
        List<ITask> _all = new List<ITask>();
        bool _isBusy;

#pragma warning disable 1591 // Xml Comments
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
       
        public IEnumerable<ITask> All { get { return _all; } }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                PropertyChanged.Notify(() => IsBusy);
            }
        }

        public TaskContext Execute(ITask task, object associatedData = null)
        {
            var context = new TaskContext(associatedData);

            _all.Add(task);
            UpdateBusy();

            task.Execute(context).ContinueWith((p,d) => {
                _all.Remove(task);
                UpdateBusy();
            });

            return context;
        }


        void UpdateBusy()
        {
            IsBusy = _all.Count > 0;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
