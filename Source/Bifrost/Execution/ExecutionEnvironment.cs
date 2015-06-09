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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IExecutionEnvironment"/>
    /// </summary>
    public class ExecutionEnvironment : IExecutionEnvironment
    {
        IFileSystem _fileSystem;

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutionEnvironment"/>
        /// </summary>
        /// <param name="fileSystem">The filesystem to use for getting assemblies at locations for the environment</param>
        public ExecutionEnvironment(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

#pragma warning disable 1591 // Xml Comments
        

        public IEnumerable<FileInfo> GetReferencedAssembliesFileInfo()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new Uri(codeBase);
            var directory = Path.GetDirectoryName(uri.LocalPath);

            var files = _fileSystem.GetFilesFrom(directory, "*.dll");
            files.Concat(_fileSystem.GetFilesFrom(directory, "*.exe"));
            return files;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
