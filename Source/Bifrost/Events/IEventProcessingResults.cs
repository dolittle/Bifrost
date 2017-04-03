/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Defines a set of <see cref="IEventProcessingResult">results</see> from processing <see cref="IEvent">events</see>
    /// </summary>
    public interface IEventProcessingResults : IEnumerable<IEventProcessingResult>
    {
    }
}
