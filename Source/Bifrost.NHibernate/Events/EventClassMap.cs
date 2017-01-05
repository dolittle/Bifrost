/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;
using FluentNHibernate.Mapping;

namespace Bifrost.NHibernate.Events
{
    public class EventClassMap : ClassMap<IEvent>
    {
        public EventClassMap()
        {
            Table("Events");
            Id(e => e.Id).GeneratedBy.Increment();
            
            Map(e => e.CommandContext);
            Map(e => e.CommandName);
            Map(e => e.Name);
            Map(e => e.EventSourceId);
            Map(e => e.EventSource);
            Map(e => e.Version).CustomType<EventSourceVersionCustomType>();
            Map(e => e.CausedBy);
            Map(e => e.Origin);
            Map(e => e.Occured);
        }
    }
}
