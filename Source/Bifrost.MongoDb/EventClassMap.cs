/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;
using MongoDB.Bson.Serialization;

namespace Bifrost.MongoDB
{
    public class EventClassMap : BsonClassMap<IEvent>
    {
        public EventClassMap()
        {
            AutoMap();
            
        }
    }
}
