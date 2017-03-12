/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Exception that gets thrown if a specific <see cref="IApplicationResourceType"/> is unknown
    /// and can't be resolved typically by a <see cref="ICanResolveApplicationResources"/>
    /// </summary>
    public class UnknownApplicationResourceType : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnknownApplicationResourceType"/>
        /// </summary>
        /// <param name="type"><see cref="Type">Type</see> of the resource type</param>
        public UnknownApplicationResourceType(Type type) : base($"Unknown application resource type of '{type.FullName}'")
        { }


        /// <summary>
        /// Initializes a new instance of <see cref="UnknownApplicationResourceType"/>
        /// </summary>
        /// <param name="identifier"><see cref="string">Identifier</see> of the resource type</param>
        public UnknownApplicationResourceType(string identifier) : base($"Unknown application resource type of '{identifier}'")
        { }
    }
}
