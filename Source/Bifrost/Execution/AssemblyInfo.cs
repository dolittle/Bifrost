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

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents the information about assemblies
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyInfo"/>
        /// </summary>
        /// <param name="name">Name of the assembly</param>
        /// <param name="path">Path to the assembly</param>
        public AssemblyInfo(string name, string path)
        {
            Name = name;
            Path = path;
            FileName = System.IO.Path.GetFileName(path);
        }

        /// <summary>
        /// Gets the name of the assembly
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the fileName of the assembly
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the fullpath to the assembly
        /// </summary>
        public string Path { get; private set; }
    }
}
