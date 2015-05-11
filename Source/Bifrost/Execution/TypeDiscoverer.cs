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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#if(SILVERLIGHT)
using System.Windows;
#endif
using System.Reflection;
using System.Threading.Tasks;
using Bifrost.Extensions;
using System.Runtime.InteropServices;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="ITypeDiscoverer">ITypeDiscoverer</see>
    /// 
    /// Uses the current AppDomain / Deployment and discoveres all types loaded
    /// </summary>
    [Singleton]
    public class TypeDiscoverer : ITypeDiscoverer
    {
        IAssemblies _assemblies;
        ITypeFinder _typeFinder;

#if(SILVERLIGHT || WINDOWS_PHONE)
        List<Type> _types;
#else
        ConcurrentBag<Type> _types;
#endif


        /// <summary>
        /// Initializes a new instance of <see cref="TypeDiscoverer">TypeDiscoverer</see>
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for getting assemblies</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding types from all collected types</param>
        public TypeDiscoverer(IAssemblies assemblies, ITypeFinder typeFinder)
        {
            _assemblies = assemblies;
            _typeFinder = typeFinder;

#if(SILVERLIGHT)
            _types = new List<Type>();
#else
            _types = new ConcurrentBag<Type>();
#endif
            CollectTypes();
        }


#pragma warning disable 1591 // Xml Comments
        public IEnumerable<Type> GetAll()
        {
            return _types;
        }

        public Type FindSingle<T>()
        {
            return _typeFinder.FindSingle<T>(_types);
        }

        public IEnumerable<Type> FindMultiple<T>()
        {
            return _typeFinder.FindMultiple<T>(_types);
        }

        public Type FindSingle(Type type)
        {
            return _typeFinder.FindSingle(_types, type);
        }

        public IEnumerable<Type> FindMultiple(Type type)
        {
            return _typeFinder.FindMultiple(_types, type);
        }

        public Type FindTypeByFullName(string fullName)
        {
            return _typeFinder.FindTypeByFullName(_types, fullName);
        }
#pragma warning restore 1591 // Xml Comments


        void AddTypes(IEnumerable<Type> types)
        {
            types.ForEach(_types.Add);
        }


#if(WINDOWS_PHONE)
        void CollectTypes()
        {
            if (null != Deployment.Current)
            {
                var parts = Deployment.Current.Parts;
                foreach (var part in parts)
                {
                    var assemblyName = part.Source.Replace(".dll", string.Empty);
                    var assembly = Assembly.Load(assemblyName);
                    AddTypes(assembly.GetTypes());
                }
            }
        }
#else

#if(SILVERLIGHT)
		void CollectTypes()
		{

			if (null != Deployment.Current)
			{
				var parts = Deployment.Current.Parts;
				foreach (var part in parts)
				{
					AddTypesFromPart(part);
				}
			}
		}

		void AddTypesFromPart(AssemblyPart part)
		{
			var info = Application.GetResourceStream(new Uri(part.Source, UriKind.Relative));
			var assembly = part.Load(info.Stream);
			AddTypes(assembly.GetTypes());
		}
#else
#if(NETFX_CORE)
		void CollectTypes()
		{
            foreach (var assembly in _assemblies.GetAll())
               AddTypes(assembly.DefinedTypes.Select(t => t.AsType());
		}
#else
        void CollectTypes()
        {
            var assemblies = _assemblies.GetAll();
            Parallel.ForEach(assemblies, assembly =>
            {
                try
                {
                    AddTypes(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                        Debug.WriteLine(string.Format("Failed to load: {0} {1}", loaderException.Source, loaderException.Message));
                }

            });
        }
#endif
#endif
#endif
    }
}
