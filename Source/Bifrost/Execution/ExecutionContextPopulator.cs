/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Execution
{
    /// <summary>
    /// Represents the method that gets called when an <see cref="IExecutionContext"/> can be populated
    /// </summary>
    /// <param name="context"></param>
    /// <param name="details"></param>
    public delegate void ExecutionContextPopulator(IExecutionContext context, dynamic details);
}
