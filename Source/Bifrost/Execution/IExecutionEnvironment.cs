#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
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
using System.IO;

namespace Bifrost.Execution
{
    /// <summary>
    /// Defines functionality needed from the execution environment
    /// </summary>
    public interface IExecutionEnvironment
    {
        /// <summary>
        /// Gets the paths to assemblies in your environment
        /// </summary>
        /// <returns>All assemblies by their paths in the environment</returns>
        IEnumerable<FileInfo> GetReferencedAssembliesFileInfo();
    }
}
