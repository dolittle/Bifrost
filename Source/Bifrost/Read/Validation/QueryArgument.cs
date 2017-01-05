/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using Bifrost.Rules;

namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Represents an argument on a query
    /// </summary>
    public class QueryArgument
    {
        /// <summary>
        /// Gets or sets the property info for the argument
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// Gets or sets the rules for the argument
        /// </summary>
        public IEnumerable<IRule> Rules { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public QueryArgumentValidationResult    Validate(IRuleContext context)
        {
            var result = new QueryArgumentValidationResult(null);

            return result;
        }
    }
}
