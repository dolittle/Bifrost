---
title: Aggregates and Aggregate Roots
description: About Aggregates and Aggregate Roots
keywords: 
author: einari, smithmx
---

# Background

An *Aggregate* is an object or collection of objects (Entities and Value Objects) that must be treated as a single unit for the
purposes of a business transaction. The *Aggregate* will maintain all invariants for all objects that comprise the *Aggregate*,
enforcing all business rules internal to the *Aggregate*.

# Aggregate Root 

All *Aggregates* have a single point of entry that is know as the root.  The *Aggregate Root* is the interface to the external world.
All interaction with an *Aggregate* is via the *Aggregate Root*.  As such, an *Aggregate Root* **MUST** have a globally unique identifier
within the system.  Other Entites that are present in the *Aggregate* but are not *Aggregate Roots* require only a locally unique
identifier, that is, an Id that is unique within the *Aggregate*.  For example, an *Order* will contain *Order Lines*.  The *Order* is
the *Aggregate Root* and must have a unique *Order Id*.  The *Order Lines* cannot be accessed directly.  Any updates to *Order Lines*
must be via the *Order*.  That is, you ask the *Order* to make the change to **IT'S** *Order Line*.  An *Order Line* is only required to
be identifiable with the *Order* therefore it is permissible to have *Order Line Id*'s such as 1,2,3 etc. on each order.  It is, of
course, valid to use a globally unique identifier onthe *Order Line* for persistence purposes, while having a locally unique candidate
key for presentation.

It is **not permissible** to have references to entities or value objects from an *Aggregate Root* other than the *Aggregate Root*
itself.  In a *CQRS* system, it is permissible, indeed required, to have read models that have data from one or more Aggregate Roots.

## Modelling

The modelling of an *Aggregate Root* is based on the invariants that must be maintained when updating.  In our example above, an *Order*
will perform aggregate functions on the order lines (e.g. summing the totals, assigning ids, maintaining ordering, etc.) and therefore will
potentially update all order lines at the same time.  An *Aggregate* is essentially a business transaction and should include everything that has
to be kept consistent.

## System Invariants

The *Aggregate* maintains only internal invariants.  It is not responsible for maintaining invariants between *Aggregates*.  For example, it
is not the responsiblity of the individual *Aggregate* to ensure that a *Username* is unique.  It is the responsibility of the **System as a whole**
to maintain these system invariants.  The exact mechanism it uses will depend on the invariant and the consequences of breaking them.

> [!Note]
> What you must learn is that these rules are no different that the rules of a computer system. Some of them can be bent. Others can be broken.
> Morpheus, The Matrix
>
> All rules are not created equal.  Business Rules are essentially business problems, not primarily technical problems.  It may be simpler and cheaper
> to break a rule and then clean it up later in a compensating transaction, than to expend a lot of time, energy and money ensuring the rule is not broken.

# Aggregate Roots in Bifrost

Bifrost provides a base class *AggregateRoot* that implements functionality that makes it easier to rehydrate Aggregate Roots, generate events and persist these
to the Event Store.  The *AggregateRoot* is an implementation of an [Event Source](./Events/event_sourcing).  It is *recommended* that all your *Aggregate Root*
classes inherit from this class.

## State

In Bifrost, an *Aggregate* is a purely *WRITE* concern.  It plays no roll in reading or presentation.  As such, an *Aggregate* **must not** expose any public
state, via variables or properties.  Neither should the *Aggregate* return any of its internal entities or values objects, even in a transient state.
The *Aggregate* is only required to keep such internal state that it requires to maintain its invariants.  Since no public state is exposed by the *Aggregate*
it does not need to keep in state values that are not

## Identity

All *AggregateRoot* classes must have a *Guid* key.  It is permissible to include other candidate keys, though these should have a map to the Guid.

## Rehydration

*AggregateRoot*s are an [Event Source](./Events/event_sourcing.md) and their internal state is rehydrated from the persistance store by replaying the events associated
with this instance.  Bifrost distinguishes between *CommittedEventStream*s, which are persisted to the [Event Store](./Events/event_store.md) and *UncommittedEventStream*s
which are not persisted.  A *CommittedEventStream* can be replayed against an *AggregateRoot* and each [Event](./Events/introduction.md) re-applied.  Since the [Event](./Events/introduction.md)
is responsible for setting an internal state, the *AggregateRoot* will be returned to its actual state when all [Events](./Events/introduction.md) are re-applied.  The
*CommittedEventStream* is a perfect audit record of all changes of the *AggregateRoot*.





