/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Strings
{
    /// <summary>
    /// Gets thrown when separators are not specified
    /// </summary>
    public class MissingSeparatorsInFormatString : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingSeparatorsInFormatString"/>
        /// </summary>
        public MissingSeparatorsInFormatString(string formatString) : base($"You must specify at least one separator in your format string '{formatString}'") { }
    }
}
