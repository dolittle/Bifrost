/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents the metadata for the LessThan validation rule
    /// </summary>
    public class LessThanOrEqual : Rule
    {
        /// <summary>
        /// Gets or sets the value that values validated up against must be less than
        /// </summary>
        public object Value { get; set; }
    }
}
