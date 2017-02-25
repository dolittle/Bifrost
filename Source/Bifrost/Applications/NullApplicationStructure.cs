/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents a null implementation of <see cref="IApplicationStructure"/>
    /// </summary>
    public class NullApplicationStructure : IApplicationStructure
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NullApplicationStructure"/>
        /// </summary>
        public NullApplicationStructure()
        {
            AllStructureFormats = new IStringFormat[0];
        }

        /// <inheritdoc/>
        public IEnumerable<IStringFormat> AllStructureFormats { get; }

        /// <inheritdoc/>
        public IEnumerable<IStringFormat> GetStructureFormatsForArea(ApplicationArea area)
        {
            return new IStringFormat[0];
        }
    }
}
