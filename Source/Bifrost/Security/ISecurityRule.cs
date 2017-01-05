/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Security
{
    /// <summary>
    /// Defines a rule for security
    /// </summary>
    public interface ISecurityRule
    {
        /// <summary>
        /// Check if a securable instance is authorized
        /// </summary>
        /// <param name="securable">The securable instance to check</param>
        /// <returns>True if has access, false if not</returns>
        bool IsAuthorized(object securable);

        /// <summary>
        /// Returns a description of the rule
        /// </summary>
        string Description { get; }

    }
}
