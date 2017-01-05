/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Validation.MetaData
{
    /// <summary>
    /// Represents the validation meta data for the Regex rule
    /// </summary>
    public class Regex : Rule
    {
        /// <summary>
        /// Gets or sets the expression that the rule represents
        /// </summary>
        public string Expression { get; set; }
    }
}
