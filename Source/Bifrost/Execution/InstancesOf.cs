/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
