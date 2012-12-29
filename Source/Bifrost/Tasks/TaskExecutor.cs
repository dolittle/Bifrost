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
using System.Linq;
using SystemTask = System.Threading.Tasks.Task;

namespace Bifrost.Tasks
{
    /// <summary>
    /// Represents a <see cref="ITaskExecutor"/>
    /// </summary>
    public class TaskExecutor : ITaskExecutor
    {
#pragma warning disable 1591 // Xml Comments
        public void Execute(Task task)
        {
            
            for (var operationIndex = task.CurrentOperation; operationIndex < task.Operations.Length; operationIndex++)
                SystemTask.Factory.StartNew(t => ((Task)t).Operations[operationIndex]((Task)t), task);
        }

        public void Stop(Task task)
        {
            throw new System.NotImplementedException();
        }
#pragma warning restore 1591 // Xml Comments
    }
}
