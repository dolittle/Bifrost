/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Defines an application structure
    /// </summary>
    public interface IApplicationStructure
    {

        /// <summary>
        /// Get all structure formats for specific identifier
        /// </summary>
        /// <param name="area"><see cref="ApplicationArea"/> to get for</param>
        /// <returns><see cref="IEnumerable{T}">String formats</see> for the identifier</returns>
        IEnumerable<IStringFormat> GetStructureFormatsForArea(ApplicationArea area);


        /// <summary>
        /// Gets the different <see cref="IStringFormat"/> representing the allowed structures of the 
        /// application
        /// </summary>
        IEnumerable<IStringFormat>  AllStructureFormats { get; }
    }
}
