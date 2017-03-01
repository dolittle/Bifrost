/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventProcessingResults"/>
    /// </summary>
    public class EventProcessingResults : IEventProcessingResults
    {
        IEnumerable<IEventProcessingResult> _results;

        /// <summary>
        /// Initializes a new instance of <see cref="EventProcessingResults"/>
        /// </summary>
        /// <param name="results"></param>
        public EventProcessingResults(IEnumerable<IEventProcessingResult> results)
        {
            _results = results;
        }

        /// <inheritdoc/>
        public IEnumerator<IEventProcessingResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }
    }
}
