/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a visitor that takes part in populating all the details for the <see cref="IExecutionContext"/>
    /// </summary>
    public interface ICanPopulateExecutionContextDetails
    {
        /// <summary>
        /// Method that gets called when the <see cref="IExecutionContext"/> is being set up
        /// </summary>
        /// <param name="executionContext"><see cref="IExecutionContext"/> that is populated</param>
        /// <param name="details">Details for the <see cref="IExecutionContext"/> to populate</param>
        void Populate(IExecutionContext executionContext, dynamic details);
    }
}
