/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bifrost.Extensions;
using Bifrost.Validation;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents the basis for a validation descriptor for describing validation for queries
    /// </summary>
    /// <typeparam name="TQuery">Type of <see cref="IQuery"/> descriptor is for</typeparam>
    public class QueryValidationDescriptorFor<TQuery> : IQueryValidationDescriptor
        where TQuery : IQuery
    {
        Dictionary<string, IValueValidationBuilder> _arguments;

        /// <summary>
        /// Initializes a new instance of <see cref="QueryValidationDescriptorFor{TQ}"/>
        /// </summary>
        public QueryValidationDescriptorFor()
        {
            _arguments = new Dictionary<string, IValueValidationBuilder>();
        }

        /// <summary>
        /// Gets the <see cref="IRuleBuilder">rule builders</see> for the <see cref="IQuery">query </see>arguments
        /// </summary>
        public IEnumerable<IValueValidationBuilder> ArgumentsRuleBuilders
        {
            get
            {
                return _arguments.Values;
            }
        }

        /// <summary>
        /// Start describing an argument on a <see cref="IQuery"/>
        /// </summary>
        /// <param name="expression">Expression pointing to the argument on the query</param>
        /// <returns>A <see cref="QueryArgumentValidationBuilder{TQ, TA}"/> for building the rules for the argument</returns>
        public QueryArgumentValidationBuilder<TQuery, TArgument> ForArgument<TArgument>(Expression<Func<TQuery, TArgument>> expression)
        {
            var property = expression.GetPropertyInfo();
            var builder = new QueryArgumentValidationBuilder<TQuery, TArgument>(property);
            _arguments[property.Name] = builder;
            return builder;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<IValueRule> ArgumentRules
        {
            get 
            {
                var rules = new List<IValueRule>();
                _arguments.Values.ForEach(r => rules.AddRange(r.Rules));
                return rules;
            }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
