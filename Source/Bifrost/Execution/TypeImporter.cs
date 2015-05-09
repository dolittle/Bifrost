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

namespace Bifrost.Execution
{
	/// <summary>
	/// Represents a <see cref="ITypeImporter">ITypeImporter</see>
	/// </summary>
	public class TypeImporter : ITypeImporter
	{
		private readonly IContainer _container;
		private readonly ITypeDiscoverer _typeDiscoverer;


		/// <summary>
		/// Initializes a new instance of <see cref="TypeImporter">TypeImporter</see>
		/// </summary>
		/// <param name="container"><see cref="IContainer"/> that used for creating types</param>
		/// <param name="typeDiscoverer">A <see cref="ITypeDiscoverer">ITypeDiscoverer</see> used for discovering types</param>
		public TypeImporter(IContainer container, ITypeDiscoverer typeDiscoverer)
		{
			_container = container;
			_typeDiscoverer = typeDiscoverer;
		}

#pragma warning disable 1591 // Xml Comments
		public T[] ImportMany<T>()
		{
			try
			{
				var types = _typeDiscoverer.FindMultiple<T>();
                ThrowIfTypeCanNotBeImported(typeof(T), types);
                var instances = types.Select(t => (T)_container.Get(t)).ToArray();
				return instances;
			}
			catch (ArgumentException innerException)
			{
                ThrowUnableToImportType(typeof(T),innerException);
			}
            return new T[0];
		}



		public T Import<T>()
		{
			try
			{
				var singleType = _typeDiscoverer.FindSingle<T>();
				if (null != singleType)
				{
					var instance = (T)_container.Get(singleType);
					return instance;
				}
				else
				{
                    ThrowCanNotBeImported(typeof(T));
				}
			}
			catch (ArgumentException innerException)
			{
                ThrowUnableToImportType(typeof(T), innerException);
			}
            return default(T);
		}
#pragma warning restore 1591 // Xml Comments

        void ThrowUnableToImportType(Type type, ArgumentException innerException)
        {
            throw new ArgumentException(
                string.Format("Can't import type {0}, see inner exception for details", type), innerException);
        }

        void ThrowIfTypeCanNotBeImported(Type type, IEnumerable<Type> types)
        {
            if (types == null) ThrowCanNotBeImported(type);
        }

        void ThrowCanNotBeImported(Type type)
        {
            throw new ArgumentException(
                string.Format("Can't import type {0}, it was not discovered", type));
        }

	}
}