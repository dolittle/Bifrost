/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bifrost.Extensions;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IContractToImplementorsMap"/>
    /// </summary>
    public class ContractToImplementorsMap : IContractToImplementorsMap
    {
        ConcurrentDictionary<Type, ConcurrentDictionary<string, Type>> _contractsAndImplementors = new ConcurrentDictionary<Type, ConcurrentDictionary<string, Type>>();
        ConcurrentDictionary<Type, Type> _allTypes = new ConcurrentDictionary<Type, Type>();

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<Type> All { get { return _allTypes.Keys; } }

        public void Feed(IEnumerable<Type> types)
        {
            MapTypes(types);
            AddTypesToAllTypes(types);
        }

        public IEnumerable<Type> GetImplementorsFor<T>()
        {
            return GetImplementorsFor(typeof(T));
        }

        public IEnumerable<Type> GetImplementorsFor(Type contract)
        {
            var implementingTypes = GetImplementingTypesFor(contract);
            return implementingTypes.Values;
        }
#pragma warning restore 1591 // Xml Comments

        void AddTypesToAllTypes(IEnumerable<Type> types)
        {
            types.ForEach(type => _allTypes[type] = type);
        }

        void MapTypes(IEnumerable<Type> types)
        {
            var implementors = types.Where(IsImplementation);
            Parallel.ForEach(implementors, implementor =>
            {
                var baseAndImplementingTypes = implementor.AllBaseAndImplementingTypes();
                baseAndImplementingTypes.ForEach(contract => GetImplementingTypesFor(contract)[GetKeyFor(implementor)] = implementor);
            });
        }

        bool IsImplementation(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return !typeInfo.IsInterface && !typeInfo.IsAbstract;
        }

        ConcurrentDictionary<string, Type> GetImplementingTypesFor(Type contract)
        {
            var implementingTypes = _contractsAndImplementors.GetOrAdd(contract, (key) => new ConcurrentDictionary<string, Type>());
            return implementingTypes;
        }

        string GetKeyFor(Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}