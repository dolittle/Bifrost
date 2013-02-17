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

using Bifrost.Configuration;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an application that configures Bifrost
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Gets the container used by the application
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// Gets the called to configure the applications
        /// </summary>
        /// <param name="configure"><see cref="IConfigure"/> configuration instance</param>
        void OnConfigure(IConfigure configure);

        /// <summary>
        /// Gets called when Application is started
        /// </summary>
        void OnStarted();

        /// <summary>
        /// Gets called when Application is stopped
        /// </summary>
        void OnStopped();
    }
}
