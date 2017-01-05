/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a <see cref="IExecutionContextDetailsPopulator"/>
    /// </summary>
    public class ExecutionContextDetailsPopulator : IExecutionContextDetailsPopulator
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        IEnumerable<Type> _populatorTypes;

        /// <summary>
        /// Initializes an instance of <see cref="ExecutionContextDetailsPopulator"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> to use for discovering implementations of <see cref="ICanPopulateExecutionContextDetails"/></param>
        /// <param name="container"><see cref="IContainer"/> to use for instantiating types</param>
        public ExecutionContextDetailsPopulator(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _populatorTypes = _typeDiscoverer.FindMultiple<ICanPopulateExecutionContextDetails>();
        }

#pragma warning disable 1591 // Xml Comments
        public void Populate(IExecutionContext executionContext, dynamic details)
        {
            foreach (var type in _populatorTypes)
            {
                var instance = _container.Get(type) as ICanPopulateExecutionContextDetails;
                instance.Populate(executionContext, details);
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
