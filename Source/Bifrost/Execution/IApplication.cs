#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
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
        void OnConfigure(Configure configure);

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
