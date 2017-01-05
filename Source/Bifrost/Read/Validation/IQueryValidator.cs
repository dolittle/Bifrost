/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Defines the system that validates a query
    /// </summary>
    public interface IQueryValidator
    {
        /// <summary>
        /// Validate a query instance
        /// </summary>
        /// <param name="query"><see cref="IQuery"/> to validate</param>
        /// <returns>The <see cref="QueryValidationResult">result</see> of the query</returns>
        QueryValidationResult Validate(IQuery query);
    }
}
