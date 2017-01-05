/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Defines a factory for creating <see cref="IProblems"/>
    /// </summary>
    public interface IProblemsFactory
    {
        /// <summary>
        /// Create new <see cref="IProblems">problems</see>
        /// </summary>
        /// <returns>An instance of <see cref="IProblems"/></returns>
        IProblems Create();
    }
}
