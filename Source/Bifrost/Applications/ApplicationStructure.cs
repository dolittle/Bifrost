/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructure"/>
    /// </summary>
    public class ApplicationStructure : IApplicationStructure
    {
        IDictionary<ApplicationArea, IEnumerable<IStringFormat>> _structureFormats;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructure"/>
        /// </summary>
        /// <param name="structureFormats"></param>
        public ApplicationStructure(IDictionary<ApplicationArea, IEnumerable<IStringFormat>> structureFormats)
        {
            _structureFormats = structureFormats;
            AllStructureFormats = structureFormats.Values.SelectMany(x => x);
        }

        /// <inheritdoc/>
        public IEnumerable<IStringFormat> AllStructureFormats { get; }

        /// <inheritdoc/>
        public IEnumerable<IStringFormat> GetStructureFormatsForArea(ApplicationArea area)
        {
            return _structureFormats[area];
        }
    }
}
