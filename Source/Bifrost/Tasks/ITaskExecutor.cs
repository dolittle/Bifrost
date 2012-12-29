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

namespace Bifrost.Tasks
{
    /// <summary>
    /// Defines an executor for executing <see cref="Task">tasks</see>
    /// </summary>
    public interface ITaskExecutor
    {
        /// <summary>
        /// Execute a <see cref="Task"/> and its <see cref="TaskOperation">operations</see>
        /// </summary>
        /// <param name="task"><see cref="Task"/> to execute</param>
        void Execute(Task task);

        /// <summary>
        /// Stops a <see cref="Task"/> that is executing
        /// </summary>
        /// <param name="task"><see cref="Task"/> to stop</param>
        void Stop(Task task);
    }
}
