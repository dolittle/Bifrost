/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Defines a reporter for reporting any <see cref="IProblems">problems</see>
    /// </summary>
    public interface IProblemsReporter
    {
        /// <summary>
        /// Clear any reported problems
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets all the problems registered
        /// </summary>
        IEnumerable<IProblems> All { get; }

        /// <summary>
        /// Report any <see cref="IProblems">problems</see>
        /// </summary>
        /// <param name="problems"><see cref="IProblems">Problems</see> to report</param>
        void Report(IProblems problems);
    }
}
