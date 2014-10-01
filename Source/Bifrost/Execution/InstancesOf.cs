#region License
//
// Copyright (c) 2008-2014, Dolittle (http://www.dolittle.com)
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
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IInstancesOf{T}"/>
    /// </summary>
    /// <typeparam name="T">Base type to discover for - must be an abstract class or an interface</typeparam>
    [Singleton]
    public class InstancesOf<T> : IInstancesOf<T>
        where T : class
    {
        IEnumerable<Type> _types;
        IContainer _container;

        /// <summary>
        /// Initalizes an instance of <see cref="HaveInstanceOf{T}"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> used for discovering types</param>
        /// <param name="container"><see cref="IContainer"/> used for managing instances of the types when needed</param>
        public InstancesOf(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _types = typeDiscoverer.FindMultiple<T>();
            _container = container;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var type in _types) yield return _container.Get(type) as T;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var type in _types) yield return _container.Get(type);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
