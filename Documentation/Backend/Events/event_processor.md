---
title: About Event Processors
description: Learn about event processors
keywords: Events
author: einari
---

# Event Processor

An *EventProcessor* is responsible for processing specific events as they gets applied to
the system.

## IProcessEvents

Bifrost has an interface called `IProcessEvents`. This interface is a marker interface and
does not hold anything you need to implement. Instead, Bifrost is by convention looking for
specific method signatures. For *event processors* it is looking for a method called
`Process`that takes the [event](event.md) it can process. Bifrost will automatically
manage and call this method. It will also manage ordering of the events, so that it calls
the method with the events coming in the correct order. It will also make sure to not
call the method twice for the same event.

Imagine you have an event that gets applied:

```csharp
public class MyEvent : Event
{
    public MyEvent(Guid eventSourceId) : base(eventSourceId) {}

    public string Something { get; set; }
}
```

An event processor would then just do the following.

```csharp
public class MyEventProcessor : IProcessEvents
{
    public void Process(MyEvent @event)
    {
        // Respond to the event
    }
}
```

You can easily add new processors as you go that deal with the same event but then
generate different models that gets used by the system.

## Coordinating dependencies

If you have ordering dependencies between processors, Bifrost does not deal with this.
You would have to explicitly deal with the coordination yourself for this. You could have
a coordinating event processor that delegates to others - just remember to not implement
the `IProcessEvents` interface for these, as they would be automatically discovered and
called by Bifrost.

## Scaling out

> [!Note]
> Details coming soon
