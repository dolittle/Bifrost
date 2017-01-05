/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents an implementation of <see cref="IQueryValidationDescriptors"/> 
    /// </summary>
    [Singleton]
    public class QueryValidationDescriptors : IQueryValidationDescriptors
    {
        Dictionary<Type, IQueryValidationDescriptor> _descriptors = new Dictionary<Type, IQueryValidationDescriptor>();

        /// <summary>
        /// Initializes an instance of <see cref="QueryValidationDescriptors"/>
        /// </summary>
        public QueryValidationDescriptors(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            var descriptors = typeDiscoverer.FindMultiple(typeof(QueryValidationDescriptorFor<>)).Where(d => d != typeof(QueryValidationDescriptorFor<>));
            descriptors.ForEach(d => {
                var queryType = d.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments()[0];
                var descriptor = container.Get(d) as IQueryValidationDescriptor;
                _descriptors[queryType] = descriptor;
            });
        }


#pragma warning disable 1591 // Xml Comments
        public bool HasDescriptorFor<TQuery>() where TQuery : IQuery
        {
            return _descriptors.ContainsKey(typeof(TQuery)); 
        }

        public IQueryValidationDescriptor GetDescriptorFor<TQuery>() where TQuery : IQuery
        {
            return _descriptors[typeof(TQuery)];
        }
#pragma warning restore 1591 // Xml Comments
    }
}
