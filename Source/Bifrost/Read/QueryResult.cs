/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Read.Validation;
using Bifrost.Rules;

namespace Bifrost.Read
{
    /// <summary>
    /// Represents the result of a query
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// Initializes an instance of <see cref="QueryResult"/>
        /// </summary>
        public QueryResult()
        {
            SecurityMessages = new string[0];
            Validation = new QueryValidationResult(new BrokenRule[0]);
        }

        /// <summary>
        /// Gets or sets the name of the query
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// Gets or sets the count of total items from a query
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the items as the result of a query
        /// </summary>
        public IEnumerable Items { get; set; }

        /// <summary>
        /// Gets or sets the exception that occured during execution
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets the messages that are related to broken security rules
        /// </summary>
        public IEnumerable<string> SecurityMessages { get; set; }

        /// <summary>
        /// Gets the <see cref="QueryValidationResult">validation result</see>
        /// </summary>
        public QueryValidationResult Validation { get; set; }

        /// <summary>
        /// Gets or sets wether or not command passed security
        /// </summary>
        public bool PassedSecurity
        {
            get { return SecurityMessages != null && !SecurityMessages.Any(); }
        }

        /// <summary>
        /// Get wether or not the query was successful or not
        /// </summary>
        public bool Success
        {
            get
            {
                return  Exception == null && 
                        Items != null && 
                        Validation.Success &&
                        PassedSecurity;
            }
        }

        /// <summary>
        /// Get wether or not the query is considered invalid in validation terms
        /// </summary>
        public bool Invalid
        {
            get { return !Validation.Success; }
        }

        /// <summary>
        /// Creates a <see cref="QueryResult"/> for a given <see cref="IQuery"/>
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> to create for</param>
        /// <returns><see cref="QueryResult"/> for the given <see cref="IQuery"/></returns>
        public static QueryResult For(IQuery query)
        {
            var result = new QueryResult
            {
                QueryName = query.GetType().Name
            };
            return result;
        }
    }
}
