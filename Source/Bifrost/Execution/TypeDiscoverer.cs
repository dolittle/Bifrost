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
        static List<string> NamespaceStartingWithExclusions = new List<string>();

        IAssemblyLocator _assemblyLocator;

#if(SILVERLIGHT || WINDOWS_PHONE)
        Dictionary<string, IDictionary<string, Type>> _types;
#else
        ConcurrentDictionary<string, IDictionary<string, Type>> _types;
#endif

        IDictionary<Type, Type[]> _implementingTypes;

        /// <summary>
        /// Exclude discovering of types in a specific namespace
        /// </summary>
        /// <param name="name">Namespace to exclude</param>
        public static void ExcludeNamespaceStartingWith(string name)
        {
            NamespaceStartingWithExclusions.Add(name);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TypeDiscoverer">TypeDiscoverer</see>
        /// </summary>
        public TypeDiscoverer(IAssemblyLocator assemblyLocator)
        {
            _assemblyLocator = assemblyLocator;

#if(SILVERLIGHT)
            _types = new Dictionary<string, IDictionary<string, Type>>();
#else
            _types = new ConcurrentDictionary<string, IDictionary<string, Type>>();
#endif

            _implementingTypes = new Dictionary<Type, Type[]>();
            CollectTypes();
        }


#pragma warning disable 1591 // Xml Comments
        public IEnumerable<Type> GetAll()
        {
            return _types.Values.SelectMany(x => x.Values.ToList());
        }

        public Type FindSingle<T>()
        {
            var type = FindSingle(typeof(T));
            return type;
        }

        public Type[] FindMultiple<T>()
        {
            var types = FindMultiple(typeof(T));
            return types;
        }

        public Type FindSingle(Type type)
        {
            var typesFound = Find(type);

            if (typesFound.Length > 1)
                throw new MultipleTypesFoundException(string.Format("More than one type found for '{0}'", type.FullName));
            return typesFound.SingleOrDefault();
        }

        public Type[] FindMultiple(Type type)
        {
            var typesFound = Find(type);
            return typesFound;
        }

        public Type FindTypeByFullName(string fullName)
        {
            if (!_types.ContainsKey(fullName)) return null;

            var match = _types[fullName].Values;

            if (match.Count > 1)
            {
                throw new Exception(string.Format("Unable to resolve '{0}'. More than one type found with the current name'", fullName));
            }

            return match.First();
        }
#pragma warning restore 1591 // Xml Comments

#if(SILVERLIGHT || WINDOWS_PHONE)

        void AddTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (_types.ContainsKey(type.FullName))
                {
                    var entry = _types[type.Assembly.FullName];
                    entry[type.Assembly.FullName] = type;
                }
                else
                {
                    _types.Add(type.FullName, new Dictionary<string, Type> {{type.Assembly.FullName, type}});
                }
            }
        }

#else


        void AddTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (!_types.TryAdd(type.FullName, new Dictionary<string, Type> { { type.Assembly.FullName, type } }))
                {
                    var entry = _types[type.FullName];

                    lock (entry)
                    {
                        entry[type.Assembly.FullName] = type;
                    }
                }

            }
        }

#endif

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
					if( ShouldAddAssembly(part.Source) )
					{
						AddTypesFromPart(part);
					}
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
            foreach (var assembly in _assemblyLocator.GetAll())
               AddTypes(assembly.DefinedTypes.Select(t => t.AsType());
		}
#else
        void CollectTypes()
        {
            var assemblies = _assemblyLocator.GetAll().Where(a => ShouldAddAssembly(a.FullName)).ToList();

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
        static bool ShouldAddAssembly(string name)
        {
            if (NameStartsWithAnExcludedNamespace(name)) return false;
            return !name.Contains("System.") && !name.Contains("mscorlib");
        }

        static bool NameStartsWithAnExcludedNamespace(string name)
        {
            return NamespaceStartingWithExclusions.Any(name.StartsWith);
        }

        Type[] Find(Type type)
        {
            Type[] typesFound;
            if (!_implementingTypes.TryGetValue(type, out typesFound))
            {
                var query = from t in _types.Values.SelectMany(a => a.Values)
                            where
                                t.HasInterface(type) &&
#if(NETFX_CORE)
                                !t.GetTypeInfo().IsInterface &&
                                !t.GetTypeInfo().IsAbstract
#else
 !t.IsInterface &&
                                !t.IsAbstract
#endif
                            select t;
                typesFound = query.ToArray();
                _implementingTypes[type] = typesFound;
            }

            return typesFound;
        }
    }
}
