#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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
using System;
#if(!SILVERLIGHT)
using System.IO;
#else
using System.Windows;
#endif
#if(NETFX_CORE)
using System.Collections.Generic;
using Windows.Storage;
#endif
using System.Linq;
using System.Reflection;



namespace Bifrost.Execution
{
	/// <summary>
    /// Represents a <see cref="IAssemblyLocator"/>
    /// </summary>
    [Singleton]
    public class AssemblyLocator : IAssemblyLocator
    {
        Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyLocator"/>
        /// </summary>
        public AssemblyLocator()
        {
            Initialize();
        }

        private void Initialize()
        {
#if(SILVERLIGHT)
            _assemblies = (from part in Deployment.Current.Parts
                          where ShouldAddAssembly(part.Source)
                          let info = Application.GetResourceStream(new Uri(part.Source, UriKind.Relative))
                          select part.Load(info.Stream)).ToArray();
#else
#if(NETFX_CORE)
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assemblies = new List<Assembly>();

            IEnumerable<StorageFile>    files = null;

            var operation = folder.GetFilesAsync();
            operation.Completed = async (r, s) => {
                var result = await r;
                files = result;
            };

            while (files == null) ;

            foreach (var file in files)
            {
                if (file.FileType == ".dll" || file.FileType == ".exe")
                {
                    var name = new AssemblyName() { Name = System.IO.Path.GetFileNameWithoutExtension(file.Name) };
                    try
                    {
                        Assembly asm = Assembly.Load(name);
                        assemblies.Add(asm);
                    }
                    catch { }
                }
            }
            _assemblies = assemblies.ToArray();
#else

            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new Uri(codeBase);
            var path = Path.GetDirectoryName(uri.LocalPath);

            var files = new DirectoryInfo(path).GetFiles("*.dll");
            files.Concat(new DirectoryInfo(path).GetFiles("*.exe"));

            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            foreach (var file in files)
            {
                var assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                if (!currentAssemblies.Any(assembly => Matches(assemblyName, assembly.GetName()))) // AssemblyName.ReferenceMatchesDefinition(assemblyName, assembly.GetName())))
                    currentAssemblies.Add(Assembly.Load(assemblyName));
            }
            _assemblies = currentAssemblies.Distinct(new AssemblyComparer()).ToArray();
#endif
#endif
        }

#pragma warning disable 1591 // Xml Comments
        public Assembly[] GetAll()
        {
            return _assemblies;
        }

        public Assembly GetWithFullName(string fullName)
        {
            var query = from a in _assemblies
                        where a.FullName == fullName
                        select a;

            var assembly = query.SingleOrDefault();
            return assembly;
        }

        public Assembly GetWithName(string name)
        {
            var query = from a in _assemblies
                        where a.FullName.Contains(name)
                        select a;

            var assembly = query.SingleOrDefault();
            return assembly;
        }

#pragma warning restore 1591 // Xml Comments

#if(SILVERLIGHT)
		private static bool ShouldAddAssembly(string name)
		{
			return !name.Contains("System.");
		}
#else
        bool Matches(AssemblyName a, AssemblyName b)
        {
            return a.ToString() == b.ToString();
        }
#endif
    }
}