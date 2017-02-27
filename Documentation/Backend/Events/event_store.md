---
title: Event Store
description: Learn about event stores
keywords: Events
author: einari
---

# Event Store

All [events](introduction.md) being applied to the system can be stored in the sequence
they were applied forming a perfect permanent audit trail; a ledger of what happened to
your system. The *EventStore* represents all the state changes through its events.
Any [EventProcessor](event_processor.md) that gets added to the system will have the
event it is capable of processing automatically replayed - making it possible for the
*EventProcessor* to generate its transient state, typically in the form of a [ReadModel](../Read/read_model.md).

## Event Sources

When resuming an [EventSource](event_source.md), all relevant events can be applied to
the *EventSource* to resume state. There are more mechanisms as well, but it proves the
importance of events as being the source of truth in Bifrost.


