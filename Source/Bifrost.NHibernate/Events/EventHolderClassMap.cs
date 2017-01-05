/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Bifrost.Events;
using FluentNHibernate.Mapping;
using NHibernate.Persister.Entity;

namespace Bifrost.NHibernate.Events
{
	public class EventHolderClassMap : ClassMap<EventHolder>
	{
		public EventHolderClassMap()
		{
			Table("Events");

            Id(p => p.Id).GeneratedBy.Increment();
            Map(p => p.Name);
            Map(p => p.CommandName);
			Map(p => p.AggregateId);
			Map(p => p.LogicalEventName);
		    Map(p => p.Generation);
			Map(p => p.EventSource);
			Map(p => p.SerializedEvent);
			Map(p => p.CausedBy);
			Map(p => p.Origin);
		    Map(p => p.Occured);
		    Map(p => p.Version);
		}
	}
}