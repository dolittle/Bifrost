/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents a null implementation of <see cref="IApplicationStructureConfigurationBuilder"/>
    /// </summary>
    public class NullApplicationStructureConfigurationBuilder : IApplicationStructureConfigurationBuilder
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public NullApplicationStructureConfigurationBuilder()
        {
        }

        /// <inheritdoc/>
        public IApplicationStructure Build()
        {
            return new NullApplicationStructure();
        }

        /// <inheritdoc/>
        public IApplicationStructureConfigurationBuilder Include(ApplicationArea area, string format)
        {
            return this;
        }
    }
}
