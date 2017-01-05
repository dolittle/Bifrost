/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
namespace Bifrost.Rules
{
    /// <summary>
    /// Represents a reason for why a <see cref="IRule"/> is broken
    /// </summary>
    public class BrokenRuleReason
    {
        /// <summary>
        /// Gets the identifier for the <see cref="BrokenRuleReason"/>
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Private constructor - so we can't instantiate these without going through create
        /// </summary>
        BrokenRuleReason() { }

        /// <summary>
        /// Creates a new instance of <see cref="BrokenRuleReason"/> from a given unique identifier
        /// </summary>
        /// <param name="id">Unique identifier of the reason - this has to be a valid Guid in string format</param>
        /// <returns>A <see cref="BrokenRuleReason"/> instance</returns>
        /// <remarks>
        /// The format of the Guid has to be : 
        /// 00000000-0000-0000-0000-000000000000
        /// </remarks>
        public static BrokenRuleReason  Create(string id)
        {
            return new BrokenRuleReason
            {
                Id = Guid.Parse(id)
            };
        }
    }
}
