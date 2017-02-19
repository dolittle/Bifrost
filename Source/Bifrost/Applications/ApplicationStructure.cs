/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructure"/>
    /// </summary>
    public class ApplicationStructure : IApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructure"/>
        /// </summary>
        /// <param name="structureFormats"></param>
        public ApplicationStructure(IEnumerable<IStringFormat> structureFormats)
        {
            StructureFormats = structureFormats;
        }

        /// <inheritdoc/>
        public IEnumerable<IStringFormat> StructureFormats { get; }
    }
}
