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
        ConcurrentBag<Type> _allTypes = new ConcurrentBag<Type>();


#pragma warning disable 1591 // Xml Comments
        public IEnumerable<Type> All { get { return _allTypes; } }

        public void Feed(IEnumerable<Type> types)
        {
            AddTypesToAllTypes(types);

            var contractQuery = GetContractQueryFor(types);
            var implementorQuery = Implementations;
            Parallel.ForEach(contractQuery, contract => CollectImplementationsFor(implementorQuery, contract));
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
            Parallel.ForEach(types, type =>
            {
                if (!_allTypes.Contains(type)) _allTypes.Add(type);
            });
        }

        IEnumerable<Type> GetContractQueryFor(IEnumerable<Type> types)
        {
            var contractQuery = types.Where(
                type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return
                        typeInfo.IsInterface ||
                        typeInfo.IsAbstract &&
                        !typeInfo.IsSealed;
                });
            return contractQuery;
        }

        void CollectImplementationsFor(IEnumerable<Type> implementorQuery, Type contract)
        {
            var implementors = implementorQuery.Where(implementor => implementor.Implements(contract));
            implementors.ForEach(implementor =>
            {
                var implementorKey = GetKeyFor(implementor);
                GetImplementingTypesFor(contract)[implementorKey] = implementor;
            });
        }

        IEnumerable<Type> Implementations
        {
            get
            {
                return _allTypes.Where(
                    type =>
                    {
                        var typeInfo = type.GetTypeInfo();
                        return !typeInfo.IsInterface && !typeInfo.IsAbstract;
                    });
            }
        }

        void CollectImplementations(Type implementor, IEnumerable<Type> contractQuery)
        {
            var contracts = implementor.AllBaseAndImplementingTypes().Union(contractQuery);
            contracts.ForEach(contract =>
            {
                var implementorKey = GetKeyFor(implementor);
                GetImplementingTypesFor(contract)[implementorKey] = implementor;
            });
        }

        ConcurrentDictionary<string, Type> GetImplementingTypesFor(Type contract)
        {
            var isNew = false;
            if (!_contractsAndImplementors.ContainsKey(contract)) isNew = true;
            var implementingTypes = _contractsAndImplementors.GetOrAdd(contract, (key) => new ConcurrentDictionary<string, Type>());

            if (isNew) CollectImplementationsFor(Implementations, contract);
            return implementingTypes;
        }

        string GetKeyFor(Type type)
        {
            return type.AssemblyQualifiedName;
        }
    }
}