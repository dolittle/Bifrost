/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeFinder"/>
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
#pragma warning disable 1591 // Xml Comments
        public Type FindSingle<T>(IContractToImplementorsMap types)
        {
            var type = FindSingle(types, typeof(T));
            return type;
        }

        public IEnumerable<Type> FindMultiple<T>(IContractToImplementorsMap types)
        {
            var typesFound = FindMultiple(types, typeof(T));
            return typesFound;
        }

        public Type FindSingle(IContractToImplementorsMap types, Type type)
        {
            var typesFound = types.GetImplementorsFor(type);
            ThrowIfMultipleTypesFound(type, typesFound);
            return typesFound.SingleOrDefault();
        }

        public IEnumerable<Type> FindMultiple(IContractToImplementorsMap types, Type type)
        {
            var typesFound = types.GetImplementorsFor(type);
            return typesFound;
        }

        public Type FindTypeByFullName(IContractToImplementorsMap types, string fullName)
        {
            var typeFound = types.All.Where(t => t.FullName == fullName).SingleOrDefault();
            ThrowIfTypeNotFound(fullName, typeFound);
            return typeFound;
        }
#pragma warning restore 1591 // Xml Comments

        void ThrowIfMultipleTypesFound(Type type, IEnumerable<Type> typesFound)
        {
            if (typesFound.Count() > 1)
                throw new MultipleTypesFoundException(string.Format("More than one type found for '{0}'", type.FullName));
        }

        void ThrowIfTypeNotFound(string fullName, Type typeFound)
        {
            if (typeFound == null) throw new UnableToResolveTypeByName(fullName);
        }
    }
}
