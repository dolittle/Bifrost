/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
    /// <summary>
    /// Defines a system for populating a <see cref="IExecutionContext"/>
    /// </summary>
    public interface IExecutionContextDetailsPopulator
    {
        /// <summary>
        /// Populate <see cref="IExecutionContext"/> and its details
        /// </summary>
        /// <param name="executionContext"><see cref="IExecutionContext"/> that is populated</param>
        /// <param name="details">Details for the <see cref="IExecutionContext"/> to populate</param>
        void Populate(IExecutionContext executionContext, dynamic details);
    }
}
