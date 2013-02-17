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

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a manager for managing an <see cref="IApplication"/>
    /// </summary>
    /// <remarks>
    /// There can only be one application in a process / app-domain
    /// </remarks>
    public interface IApplicationManager
    {
        /// <summary>
        /// Get the current application
        /// </summary>
        /// <returns>The instance of an application</returns>
        /// <exception cref="MultipleApplicationsFoundException"/>
        IApplication Get();

        /// <summary>
        /// Set the current application
        /// </summary>
        /// <param name="application">The instance of the application</param>
        void Set(IApplication application);
    }
}