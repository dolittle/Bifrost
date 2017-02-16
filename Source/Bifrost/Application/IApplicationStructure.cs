/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Bifrost.Strings;

namespace Bifrost.Application
{
    /// <summary>
    /// Defines an application structure
    /// </summary>
    public interface IApplicationStructure
    {
        /// <summary>
        /// Gets the different <see cref="IStringFormat"/> representing the allowed structures of the 
        /// application
        /// </summary>
        IEnumerable<IStringFormat>  StructureFormats { get; }
    }
}
