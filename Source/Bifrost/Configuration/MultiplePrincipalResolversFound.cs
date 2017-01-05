/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Configuration
{
    /// <summary>
    /// Exception that is thrown when the system finds more than one principal resolver
    /// </summary>
    public class MultiplePrincipalResolversFound : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultiplePrincipalResolversFound"/>
        /// </summary>
        public MultiplePrincipalResolversFound() : base("More than one implementation of ICanResolvePrincipal found - there can only be one") { }
    }
}
