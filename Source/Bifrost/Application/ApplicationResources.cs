/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Application
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResources"/>
    /// </summary>
    public class ApplicationResources : IApplicationResources
    {
        /// <inheritdoc/>
        public ApplicationResourceIdentifierFor<T> Identify<T>(T resource)
        {
            // Get namespace 

            // Resolve namespace into location segments

            // Get type and get correct application resource for it

            throw new NotImplementedException();
        }
    }
}
