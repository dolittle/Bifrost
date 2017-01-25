/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Extensions;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents an implementation of <see cref="IOrderedInstancesOf{T}"/>.
    /// </summary>
    /// <typeparam name="T">Base type to discover for - must be an abstract class or an interface.</typeparam>
    public class OrderedInstancesOf<T> : IOrderedInstancesOf<T> where T : class
    {
        readonly IEnumerable<Type> _types;
        readonly IContainer _container;

        /// <summary>
        /// Initializes an instance of <see cref="OrderedInstancesOf{T}"/>.
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> used for discovering types.</param>
        /// <param name="container"><see cref="IContainer"/> used for managing instances of the types when needed.</param>
        public OrderedInstancesOf(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _types = typeDiscoverer.FindMultiple<T>();
            _container = container;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerator<T> GetEnumerator()
        {
            IList<Type> queue = _types.OrderBy(Order).ToList();
            ISet<Type> initialized = new HashSet<Type>();

            while (queue.Count > 0)
            {
                var progress = false;
                var ready = queue
                    .Where(s => s
                        .GetAttributes<AfterAttribute>()
                        .SelectMany(a => a.DependantTypes)
                        .All(initialized.Contains))
                    .ToList();
                foreach (var type in ready)
                {
                    yield return _container.Get(type) as T;
                    initialized.Add(type);
                    queue.Remove(type);
                    progress = true;
                }

                if (!progress)
                {
                    throw new CyclicDependencyException(
                        $"Circular dependency detected when ordering instances of {typeof(T)} between these types: {string.Join(", ", queue)}");
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
#pragma warning restore 1591 // Xml Comments

        static int Order(Type type)
        {
            return type.GetAttributes<OrderAttribute>().Select(a => a.Order).FirstOrDefault();
        }
    }
}
