/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Commands.Diagnostics
{
    /// <summary>
    /// Represents the metadata for a <see cref="IProblem"/> generated for a <see cref="ICommand"/>
    /// </summary>
    public class CommandProblemMetaData
    {
        /// <summary>
        /// Get the metadata for a specific type
        /// </summary>
        /// <param name="type">Type to get from</param>
        /// <returns>The metadata associated with the type for a problem</returns>
        public static object From(Type type)
        {
            return new { Name = type.Name, Namespace = type.Namespace };
        }
    }
}
