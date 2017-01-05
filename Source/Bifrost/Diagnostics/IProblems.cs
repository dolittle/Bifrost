/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Defines a collection of <see cref="Problem">problems</see>
    /// </summary>
    public interface IProblems : IEnumerable<Problem>
    {
        /// <summary>
        /// Report a <see cref="Problem"/>
        /// </summary>
        /// <param name="type"><see cref="ProblemType">Type of problem</see> to report</param>
        /// <param name="data">Data associated with the problem</param>
        void Report(ProblemType type, object data);

        /// <summary>
        /// Gets wether or not it has any problems
        /// </summary>
        bool Any { get; }
    }
}
