---
title: Event Sourcing
description: Learn about event sourcing
keywords: Events, Event Sourcing
author: einari
---

# Event Sourcing

State transitions of a system can be represented as [events](introduction.md).
The *EventSource* is responsible for creating these and applying them in the right order.
From the *EventSource* the events can then be stored in the right order in an
[EventStore](event_store.md).

## EventSource

As the name implies, an *EventSource* is the source of events. It is the entity
that generates events. The events are to be considered the soure of truth and the
*EventSource* representing this. In an event sourced system, nothing else can
represent the true state of the system. The *EvenSource* is responsible for applying
the state changes.

## AggregateRoot

The *AggregateRoot* is probably the most common *EventSource*. It models the things
that belong together and applies the relevant state changes. In Domain Driven Design,
you'll see that the *AggregateRoot* aggregates the entities that belong together.
This makes it easier to model business processes. The *AggregateRoot* is not the noun
in your system, but representing actual processes. This makes boundaries very much clearer
and we leave behind technical challenges like concurrency, as we have explicitly modelled
what goes together and we can only change the relevant state as a consequence.