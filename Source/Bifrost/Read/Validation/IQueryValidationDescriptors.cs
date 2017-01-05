/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Read.Validation
{
    /// <summary>
    /// Defines a system for accessing validation descriptors for queries
    /// </summary>
    public interface IQueryValidationDescriptors
    {
        /// <summary>
        /// Checks if there is a <see cref="QueryValidationDescriptor{TQ}"/> for a specific <see cref="IQuery"/> by its type
        /// </summary>
        /// <typeparam name="TQuery">Type of <see cref="IQuery"/> to check for</typeparam>
        /// <returns>True if there is a <see cref="QueryValidationDescriptor{TQ}"/> for the query and false if not</returns>
        bool HasDescriptorFor<TQuery>() where TQuery : IQuery;

        /// <summary>
        /// Get a <see cref="QueryValidationDescriptor{TQ}"/> for a specific <see cref="IQuery"/> by its type
        /// </summary>
        /// <typeparam name="TQuery">Type of <see cref="IQuery"/> to get for</typeparam>
        /// <returns><see cref="IQueryValidationDescriptor"/> describing the validation for the <see cref="IQuery"/></returns>
        IQueryValidationDescriptor GetDescriptorFor<TQuery>() where TQuery : IQuery;
    }
}
