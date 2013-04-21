#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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
using Bifrost.Tasks;

namespace Bifrost.Mimir.Web.Tasks
{
    public class TaskStatusReporter : ITaskStatusReporter
    {
        public void Started(Task task)
        {
            TaskHub.Started(task);
        }

        public void Stopped(Task task)
        {
            TaskHub.Stopped(task);
        }

        public void Paused(Task task)
        {
            
        }

        public void Resumed(Task task)
        {
            
        }

        public void StateChanged(Task task)
        {
            TaskHub.StateChanged(task);
        }
    }
}