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
using System.Collections.Generic;
using System.Linq;
#if(SILVERLIGHT)
using System.Windows;
#endif
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
        readonly IAssemblyLocator _assemblyLocator;
        readonly static List<string> NamespaceStartingWithExclusions = new List<string>();
		List<Type> _types;
        Dictionary<Type, Type[]> _typesImplementingInterface;

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
		    _types = new List<Type>();
            _typesImplementingInterface = new Dictionary<Type, Type[]>();
			CollectTypes();
		}


#pragma warning disable 1591 // Xml Comments
        public IEnumerable<Type> GetAll()
        {
            return _types;
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
#pragma warning restore 1591 // Xml Comments

#if(WINDOWS_PHONE)
        private void CollectTypes()
        {
            if (null != Deployment.Current)
            {
                var parts = Deployment.Current.Parts;
                foreach (var part in parts)
                {
                    var assemblyName = part.Source.Replace(".dll", string.Empty);
                    var assembly = Assembly.Load(assemblyName);
                    var types = assembly.GetTypes();
                    _types.AddRange(types);
                }
            }
        }
#else

#if(SILVERLIGHT)
		private void CollectTypes()
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

		private void AddTypesFromPart(AssemblyPart part)
		{
			var info = Application.GetResourceStream(new Uri(part.Source, UriKind.Relative));
			var assembly = part.Load(info.Stream);
			var types = assembly.GetTypes();
			_types.AddRange(types);
		}
#else
		private void CollectTypes()
		{
            //var assemblies = _assemblyLocator.GetAll();
            //var query = from a in assemblies
            //            where ShouldAddAssembly(a.FullName)
            //            select a;

            //foreach (var assembly in query)
            //{
            //    try
            //    {
            //        _types.AddRange(assembly.GetTypes());
            //    } catch
            //    {
            //        throw;
            //    }

            //}

		    _types =
		        _assemblyLocator.GetAll().Where(a => ShouldAddAssembly(a.FullName)).SelectMany(a => a.GetTypes()).ToList();

		    var interfaces = _types.SelectMany(t => t.GetInterfaces()).Where(i => !i.IsGenericType).Distinct();
		    _typesImplementingInterface = interfaces.ToDictionary(i => i, i => _types.Where(t => t.HasInterface(i) && !t.IsInterface && !t.IsAbstract).ToArray());
		}

#endif
#endif
		private static bool ShouldAddAssembly(string name)
		{
			if (NameStartsWithAnExcludedNamespace(name)) return false;
			return !name.Contains("System.") && !name.Contains("mscorlib");
		}

		static bool NameStartsWithAnExcludedNamespace(string name)
		{
			return NamespaceStartingWithExclusions.Any(name.StartsWith);
		}

		private Type[] Find(Type type)
		{
            //var query = from t in _types
            //            where t.HasInterface(type) && !t.IsInterface && !t.IsAbstract
            //            select t;
            //var typesFound = query.ToArray();
            //return typesFound;

		    Type[] types;
            return _typesImplementingInterface.TryGetValue(type, out types) ? types : new Type[0];
		}
	}
}
