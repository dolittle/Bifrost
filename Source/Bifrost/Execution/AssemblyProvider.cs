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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Bifrost.Collections;
using Bifrost.Extensions;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IAssemblyProvider"/>
    /// </summary>
    [Singleton]
    public class AssemblyProvider : IAssemblyProvider
    {
        _AppDomain _appDomain;
        IAssemblyFilters _assemblyFilters;
        ObservableCollection<_Assembly> _assemblies = new ObservableCollection<_Assembly>();

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyProvider"/>
        /// </summary>
        public AssemblyProvider(_AppDomain appDomain, IAssemblyFilters assemblyFilters)
        {
            _appDomain = appDomain;
            _assemblyFilters = assemblyFilters;
            appDomain.AssemblyLoad += AssemblyLoaded;

            Populate();
        }


#pragma warning disable 1591 // Xml Comments
        public IObservableCollection<_Assembly> GetAll()
        {
            return _assemblies;
        }

#pragma warning restore 1591 // Xml Comments
        void AssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            _assemblies.Add(args.LoadedAssembly);
        }

        void Populate()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new Uri(codeBase);
            var path = Path.GetDirectoryName(uri.LocalPath);

            IEnumerable<FileInfo> files = new DirectoryInfo(path).GetFiles("*.dll");
            files.Concat(new DirectoryInfo(path).GetFiles("*.exe"));
            files = files.Where(AssemblyDetails.IsAssembly);

            var currentAssemblies = _appDomain.GetAssemblies().Where(a=>_assemblyFilters.ShouldInclude(a.GetName().Name)).ToList();
            foreach (var file in files)
            {
                if (!_assemblyFilters.ShouldInclude(file.Name)) continue;

                var assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                if (!currentAssemblies.Any(assembly => Matches(assemblyName, assembly.GetName())))
                    currentAssemblies.Add(Assembly.Load(assemblyName));
            }

            var assemblies = currentAssemblies.Distinct(new AssemblyComparer());
            assemblies.ForEach(_assemblies.Add);
        }


        bool Matches(AssemblyName a, AssemblyName b)
        {
            return a.ToString() == b.ToString();
        }
    }
}
