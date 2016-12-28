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
    /// Represents an implementation of <see cref="ICanProvideAssemblies"/> that is capable of
    /// discovering assembly files from the directory in which the application resides
    /// </summary>
    public class FileSystemAssemblyProvider : ICanProvideAssemblies
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FileSystemAssemblyProvider"/>
        /// </summary>
        /// <param name="fileSystem"></param>
        public FileSystemAssemblyProvider(IFileSystem fileSystem)
        {
            var codeBase = typeof(FileSystemAssemblyProvider).GetTypeInfo().Assembly.CodeBase;
            var uri = new Uri(codeBase);

            var assemblyFileInfo = new FileInfo(uri.LocalPath);

            var assemblyFiles = fileSystem.GetFilesFrom(assemblyFileInfo.Directory.ToString(), "*.dll").ToList();
            assemblyFiles.AddRange(fileSystem.GetFilesFrom(assemblyFileInfo.Directory.ToString(), "*.exe"));

            AvailableAssemblies = assemblyFiles.Select(file => new AssemblyInfo(Path.GetFileNameWithoutExtension(file.FullName), file.FullName));
        }

#pragma warning disable 1591 // Xml Comments
        public event AssemblyAdded AssemblyAdded = (a) => { };

        public IEnumerable<AssemblyInfo> AvailableAssemblies { get; private set; }

        public Assembly Get(AssemblyInfo assemblyInfo)
        {
            return Assembly.Load(new AssemblyName(assemblyInfo.Name));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
