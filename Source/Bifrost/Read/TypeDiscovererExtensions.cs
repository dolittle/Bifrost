/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Read
{
    /// <summary>
    /// Extension methods for <see cref="ITypeDiscoverer"/> for dealing with ReadModels and Queries
    /// </summary>
    public static class TypeDiscovererExtensions
    {
        /// <summary>
        /// Get the type of the query matching the fullname.  This can be in any loaded assembly and does not require the assmebly qualified name.
        /// </summary>
        /// <param name="typeDiscoverer">instance of <see cref="ITypeDiscoverer"/> being extended</param>
        /// <param name="fullName">The full name of the type</param>
        /// <returns>the type if found, <see cref="UnknownQueryException" /> if not found or type is not a query</returns>
        public static Type GetQueryTypeByName(this ITypeDiscoverer typeDiscoverer, string fullName)
        {
            var queryType = typeDiscoverer.FindTypeByFullName(fullName);

            if (queryType == null || !queryType.HasInterface(typeof(IQueryFor<>)))
                throw new UnknownQueryException(fullName);

            return queryType;
        }

        /// <summary>
        /// Get the type of the <see cref="IReadModelOf{T}"/> matching the fullname.  This can be in any loaded assembly and does not require the assmebly qualified name.
        /// </summary>
        /// <param name="typeDiscoverer">instance of <see cref="ITypeDiscoverer"/> being extended</param>
        /// <param name="fullName">The full name of the type</param>
        /// <returns>the type if found, <see cref="UnknownReadModelOfException" /> if not found or type is not a readmodelof</returns>
        public static Type GetReadModelOfTypeByName(this ITypeDiscoverer typeDiscoverer, string fullName)
        {
            var readModelOfType = typeDiscoverer.FindTypeByFullName(fullName);

            if (readModelOfType == null || !readModelOfType.HasInterface(typeof(IReadModelOf<>)))
                throw new UnknownReadModelOfException(fullName);

            return readModelOfType;
        }
    }
}