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
using Bifrost.Execution;
using Bifrost.Tasks;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ITasksConfiguration"/>
    /// </summary>
    public class TasksConfiguration : ConfigurationStorageElement, ITasksConfiguration
    {
#pragma warning disable 1591 // Xml Comments
        public override void Initialize(IContainer container)
        {
            if (EntityContextConfiguration != null)
                EntityContextConfiguration.BindEntityContextTo<TaskEntity>(container);

            base.Initialize(container);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
