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
using System.Linq;
#if(SILVERLIGHT)
using System.Windows;
#endif
using Bifrost.Extensions;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeFinder"/>
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
#pragma warning disable 1591 // Xml Comments
        public Type FindSingle<T>(IEnumerable<Type> types)
        {
            var type = FindSingle(types, typeof(T));
            return type;
        }

        public IEnumerable<Type> FindMultiple<T>(IEnumerable<Type> types)
        {
            var typesFound = FindMultiple(types, typeof(T));
            return typesFound;
        }

        public Type FindSingle(IEnumerable<Type> types, Type type)
        {
            var typesFound = Find(types, type);
            ThrowIfMultipleTypesFound(type, typesFound);
            return typesFound.SingleOrDefault();
        }

        public IEnumerable<Type> FindMultiple(IEnumerable<Type> types, Type type)
        {
            var typesFound = Find(types, type);
            return typesFound;
        }

        public Type FindTypeByFullName(IEnumerable<Type> types, string fullName)
        {
            var typeFound = types.Where(t => t.FullName == fullName).SingleOrDefault();
            ThrowIfTypeNotFound(fullName, typeFound);
            return typeFound;
        }
#pragma warning restore 1591 // Xml Comments
        Type[] Find(IEnumerable<Type> types, Type type)
        {
            Func<Type, Type, bool> isAssignableFrom = (t1, t2) => t2.IsAssignableFrom(t1);
            if (type.IsInterface) isAssignableFrom = (t1, t2) => t1.HasInterface(t2);

            var query = types.Where(
                t=>isAssignableFrom(t,type) &&
#if(NETFX_CORE)
                !t.GetTypeInfo().IsInterface &&
                !t.GetTypeInfo().IsAbstract

#else
                !t.IsInterface &&
                !t.IsAbstract
#endif
            );

            var typesFound = query.ToArray();
            return typesFound;
        }

        void ThrowIfMultipleTypesFound(Type type, Type[] typesFound)
        {
            if (typesFound.Length > 1)
                throw new MultipleTypesFoundException(string.Format("More than one type found for '{0}'", type.FullName));
        }

        void ThrowIfTypeNotFound(string fullName, Type typeFound)
        {
            if (typeFound == null) throw new UnableToResolveTypeByName(fullName);
        }
    }
}
