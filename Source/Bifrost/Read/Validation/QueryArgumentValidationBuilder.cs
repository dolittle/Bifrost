/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using Bifrost.Validation;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents a builder for building validation description for a query
    /// </summary>
    public class QueryArgumentValidationBuilder<TQuery, TArgument> : ValueValidationBuilder<TArgument>
        where TQuery : IQuery
    {
        /// <summary>
        /// Initializes a new instance of <see cref="QueryArgumentValidationBuilder{TQuery, TArgument}"/>
        /// </summary>
        /// <param name="property">Property that represents the argument</param>
        public QueryArgumentValidationBuilder(PropertyInfo property) : base(property)
        {

        }
    }
}
