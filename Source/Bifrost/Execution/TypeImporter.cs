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
				if( types == null )
				{
					throw new ArgumentException(
						string.Format("Can't import type {0}, it was not discovered", typeof(T)));
				}
				var instances = new T[types.Length];
				for (var instanceIndex = 0; instanceIndex < types.Length; instanceIndex++)
				{
					instances[instanceIndex] = (T) _container.Get(types[instanceIndex]);
				}

				return instances;
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException(
					string.Format("Can't import type {0}, see inner exception for details", typeof(T)), innerException);
			}
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
					throw new ArgumentException(
						string.Format("Can't import type {0}, it was not discovered", typeof(T)));
				}
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException(
					string.Format("Can't import type {0}, see inner exception for details", typeof(T)), innerException);
			}
		}
#pragma warning restore 1591 // Xml Comments
	}
}