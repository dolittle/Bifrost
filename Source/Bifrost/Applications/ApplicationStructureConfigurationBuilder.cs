/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructureConfigurationBuilder"/>
    /// </summary>
    public class ApplicationStructureConfigurationBuilder : IApplicationStructureConfigurationBuilder
    {

        /// <inheritdoc/>
        public IApplicationStructure Build()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IApplicationStructureConfigurationBuilder Include(string format)
        {
            throw new NotImplementedException();
        }
    }
}