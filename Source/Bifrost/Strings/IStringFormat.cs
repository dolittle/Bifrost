/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Strings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStringFormat
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<ISegment>   Segments { get; }
    }
}
