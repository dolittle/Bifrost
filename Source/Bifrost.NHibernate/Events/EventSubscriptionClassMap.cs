/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;
using FluentNHibernate.Mapping;
using Bifrost.NHibernate.UserTypes;

namespace Bifrost.NHibernate.Events
{
    public class EventSubscriptionClassMap : ClassMap<EventSubscription>
    {
        public EventSubscriptionClassMap()
        {
            Table("EventSubscriptions");
            Id(e => e.Id).GeneratedBy.Assigned();
            Map(e => e.Owner).CustomType<TypeUserType>();
            Map(e => e.Method).CustomType<MethodInfoUserType>();
            Map(e => e.EventType).CustomType<TypeUserType>();
            Map(e => e.EventName);
            Map(e => e.LastEventId);
        }
    }
}
