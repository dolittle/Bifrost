/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Represents a problem in the system
    /// </summary>
    public class Problem
    {
        /// <summary>
        /// Gets or sets the <see cref="ProblemType">type of problem</see>
        /// </summary>
        public ProblemType Type { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the problem
        /// </summary>
        public object Data { get; set; }
    }
}
