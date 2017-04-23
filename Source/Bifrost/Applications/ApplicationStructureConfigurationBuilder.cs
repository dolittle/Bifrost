/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Logging;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructureConfigurationBuilder"/>
    /// </summary>
    public class ApplicationStructureConfigurationBuilder : IApplicationStructureConfigurationBuilder
    {
        IDictionary<ApplicationArea, IEnumerable<IStringFormat>> _structureFormats;


        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public ApplicationStructureConfigurationBuilder()
        {
            _structureFormats = new Dictionary<ApplicationArea, IEnumerable<IStringFormat>>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureConfigurationBuilder"/>
        /// </summary>
        /// <param name="structureFormats"></param>
        public ApplicationStructureConfigurationBuilder(IDictionary<ApplicationArea, IEnumerable<IStringFormat>> structureFormats)
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
        public IApplicationStructureConfigurationBuilder Include(ApplicationArea area, string format)
        {
            Logger.Internal.Trace($"Include '{format}' for '{area}'");

            if ( !format.StartsWith("[")) format = $"[.]{format}";
            var formatsByArea = new Dictionary<ApplicationArea, IEnumerable<IStringFormat>>(_structureFormats);
            var parser = new StringFormatParser();
            var stringFormat = parser.Parse(format);

            List<IStringFormat> formats;
            if (formatsByArea.ContainsKey(area))
                formats = new List<IStringFormat>(formatsByArea[area]);
            else
                formats = new List<IStringFormat>();

            formats.Add(stringFormat);
            formatsByArea[area] = formats;

            var builder = new ApplicationStructureConfigurationBuilder(formatsByArea);
            return builder;
        }
    }
}