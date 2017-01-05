/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Mapping
{
    /// <summary>
    /// The exception that is thrown when one asks for a map for unknown combination of source and target
    /// </summary>
    public class MissingMapException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingMapException"/>
        /// </summary>
        /// <param name="source"><see cref="Type">Source type</see></param>
        /// <param name="target"><see cref="Type">Target type</see></param>
        public MissingMapException(Type source, Type target) : base(string.Format("Missing map for given combination of '{0}' (SOURCE) and '{1}' (TARGET)", source.FullName, target.FullName))
        {

        }
    }
}
