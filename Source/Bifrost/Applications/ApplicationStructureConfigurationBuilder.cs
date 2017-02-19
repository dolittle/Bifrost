/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructureConfigurationBuilder"/>
    /// </summary>
    public class ApplicationStructureConfigurationBuilder : IApplicationStructureConfigurationBuilder
    {
        IEnumerable<IStringFormat> _structureFormats;


        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public ApplicationStructureConfigurationBuilder()
        {
            _structureFormats = new IStringFormat[0];
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureConfigurationBuilder"/>
        /// </summary>
        /// <param name="structureFormats"></param>
        public ApplicationStructureConfigurationBuilder(IEnumerable<IStringFormat> structureFormats)
        {
            _structureFormats = structureFormats;
        }


        /// <inheritdoc/>
        public IApplicationStructure Build()
        {
            var applicationStructure = new ApplicationStructure(_structureFormats);
            return applicationStructure;
        }

        /// <inheritdoc/>
        public IApplicationStructureConfigurationBuilder Include(string format)
        {
            var formats = new List<IStringFormat>(_structureFormats);
            var parser = new StringFormatParser();
            var stringFormat = parser.Parse(format);
            formats.Add(stringFormat);
            var builder = new ApplicationStructureConfigurationBuilder(formats);
            return builder;
        }
    }
}