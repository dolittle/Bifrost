/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a null implementation of <see cref="IEventSubscriptions"/>
    /// </summary>
    public class NullEventSubscriptions : IEventSubscriptions
    {
#pragma warning disable 1591 // Xml Comments
        public IEnumerable<EventSubscription> GetAll()
        {
            return new EventSubscription[0];
        }

        public void Save(EventSubscription subscription)
        {

        }

        public void ResetLastEventForAllSubscriptions()
        {
        }
#pragma warning restore 1591 // Xml Comments


    }
}
