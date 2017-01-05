/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

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
        IContractToImplementorsMap _contractToImplementorsMap;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeDiscoverer">TypeDiscoverer</see>
        /// </summary>
        /// <param name="assemblies"><see cref="IAssemblies"/> for getting assemblies</param>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding types from all collected types</param>
        /// <param name="contractToImplementorsMap"><see cref="IContractToImplementorsMap"/> for keeping track of the relationship between contracts and implementors</param>
        public TypeDiscoverer(IAssemblies assemblies, ITypeFinder typeFinder, IContractToImplementorsMap contractToImplementorsMap)
        {
            _assemblies = assemblies;
            _typeFinder = typeFinder;
            _contractToImplementorsMap = contractToImplementorsMap;

            CollectTypes();
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<Type> GetAll()
        {
            return _contractToImplementorsMap.All;
        }

        public Type FindSingle<T>()
        {
            return _typeFinder.FindSingle<T>(_contractToImplementorsMap);
        }

        public IEnumerable<Type> FindMultiple<T>()
        {
            return _typeFinder.FindMultiple<T>(_contractToImplementorsMap);
        }

        public Type FindSingle(Type type)
        {
            return _typeFinder.FindSingle(_contractToImplementorsMap, type);
        }

        public IEnumerable<Type> FindMultiple(Type type)
        {
            return _typeFinder.FindMultiple(_contractToImplementorsMap, type);
        }

        public Type FindTypeByFullName(string fullName)
        {
            return _typeFinder.FindTypeByFullName(_contractToImplementorsMap, fullName);
        }
#pragma warning restore 1591 // Xml Comments


        void CollectTypes()
        {
            var assemblies = _assemblies.GetAll();
            Parallel.ForEach(assemblies, assembly =>
            {
                try
                {
                    _contractToImplementorsMap.Feed(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                        Debug.WriteLine(string.Format("Failed to load: {0} {1}", loaderException.Source, loaderException.Message));
                }
            });
        }
    }
}
