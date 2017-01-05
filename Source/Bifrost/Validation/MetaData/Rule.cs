/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents the base class of a rule
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// Gets or sets the message that will be used when rule is not valid
        /// </summary>
        public string Message { get; set; }
    }
}
