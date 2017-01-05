/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.Diagnostics
{
    /// <summary>
    /// Severity of a <see cref="Problem"/>
    /// </summary>
    public enum ProblemSeverity
    {
        /// <summary>
        /// A suggestion meerly represents something for the developer to consider
        /// </summary>
        Suggestion,

        /// <summary>
        /// A warning should be considered something for the developer to really look into and most likely fix
        /// </summary>
        Warning,
        
        /// <summary>
        /// A problem marked as critical is something the developer should give immediate attention
        /// </summary>
        Critical
    }
}
