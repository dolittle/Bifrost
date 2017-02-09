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
    public class MissingSeparator : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingSeparator"/>
        /// </summary>
        public MissingSeparator() : base("You must specify at least one separator") { }
    }
}
