---
title: Resources
description: About Application Resources
keywords: 
author: einari
---
# Resources

> [!Note]
> The key words “MUST”, “MUST NOT”, “REQUIRED”, “SHALL”, “SHALL NOT”, “SHOULD”, “SHOULD NOT”,
“RECOMMENDED”, “MAY”, and “OPTIONAL” in this document are to be interpreted as described in
[RFC 2119](https://tools.ietf.org/html/rfc2119).

All artifacts one create in an application are identified as application resources.
These resources are represented by what is called an **Application Resource Identifier**.
This identifier keeps information about the location of a resource, its logical name and
what well known type it is.

The identifiers are used throughout the runtime when using things like [Commands](../Commands/introduction.md), [EventSources](../Events/event_sourcing.md) and [Events](../Events/event.md). For instance, when an Event is persisted to the [store](../Events/event_store.md); metadata related to the event is stored with it - such as the **Application Resource Identifier**. The reason for storing this type of information rather than the language specific information, such as the .NET CLR information is that it leaves us with less flexibility in the code for refactoring. Once an event is stored and we need to replay an event from the store, if it sits there with Type information and it has been moved - there is no way to identify it and get back to the actual type.

In addition to this, it opens up the opportunity for Bifrost to be a runtime hosting other languages as well. By decoupling from language structures and having a common representation of resources, we can then go between different platforms.


## Domain Driven

The structure of an **Application Resource Identifier** is focused around your actual domain.

> [!Note]
> Read more [here](../../Articles/overview.md) about domain driven design in Bifrost.

It all starts with a bounded context and within it you can have one or more modules and with each of these one or more features. Within each feature you can also have a recursive number of sub features. For all of these levels in the hierarchy you can have resources that live. 

Every resource in the system has a specific purpose in the pipeline and often they should also be segregated from other resources. They are therefor identified to belong to a certain [area](#Well-known-areas).

The structure would look like the following for each of the areas.


    +-- Bounded Context
    |   - Resource
    |   +-- Module
    |   |   - Resource
    |   +---- Feature
    |   |     - Resource
    |   +------ Sub feature
    |   |       - Resource


## Configuration

Bifrost relies heavily on conventions and some of these conventions are configurable in how it resolves things. Application Resources is a good example of something that can be configured and in 
fact needs to have some configuration in order for it to be able to resolve resources back and forth from programming language specific metadata to Bifrost specific metadata.

In order for Bifrost to be able to take a programming langauge specific type and turn it into a **Application Resource Identifier**, it needs to be able to turn the metadata from the type info into strings and be able to extract the details from it into an identifier. This is configured using a special string expression.

> [!Note]
> It is also possible to configure it using the fluent interface. That is not covered by the documentation at this point

The format is based on well known identifiers which it recognizes:

* BoundedContext
* Module
* Feature
* SubFeature

All these has to be represented as a variable according to the.
Example:

"{BoundedContext}.-^{Module}.-^{Feature}.-^{SubFeature}*

This is telling that the BoundedContext is required, while the rest are optionals and the last SubFeature is recursive and also depends on the existence of a Feature. SubFeatures belong to a Feature.

One format *MUST* include a BoundedContext - there can only be one of it.
One format *CAN* include Module, and there *MUST* only be one of it
One format *CAN* include Feature, but only if there is a Module before in the string format, and there *MUST* only be one of it
One format *CAN* include multiple SubFeatures, but only if there is a Feature before in the string format

Configuring this in C# using an `ICanConfigure`implementation:

```csharp
public class Configurator : ICanConfigure
{
    public void Configure(IConfigure configure)
    {
        configure
            .Application("ApplicationName",
                a => a.Structure(s => s
                    .Domain("Domain.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                    .Events("Events.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                    .Read("Read.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
                    .Frontend("Web.{BoundedContext}.-{Module}.-{Feature}.^{SubFeature}*")
            ));

        // Other configuration...
    }
}
```

This configuration basically tells how it can resolve a C# namespace into the location
of resources. In addition to that, make note of the application name, which is also part of the identifier.

> [!Note]
> Notice that the fluent interface has support for the [well known areas](#Well-known-areas).

It is possible to have multiple locations for an area. You just simply add another line with a configuration matching up with your namespace and type information.

## Working with resources

Inside Bifrost you'll find a set of tools that can help you work with resources. Normally you would not need to do this, as all the resource management is taken care of for you. But if you have a specific need in your application to work directly with it - you have a few options to do so.

### Identifying types

If you have a language specific representation of a resource, for instance an Event in C#:

```csharp
namespace Events.MyBoundedContext
{
    public class MyEvent : Event
    {

    }
}
```

You can identify this by taking a dependency on `IApplicationResources` and use one of the methods to identify it; either by the instance or by its type:

```csharp
using Bifrost.Applications;
using Events.MyBoundedContext;

namespace MyApplication
{
    public class MySystem 
    {
        public MySystem(IApplicationResources applicationResources)
        {
            // Identify the resource from a CLR type
            var identifier = applicationResources.Identify(typeof(MyEvent));
        }
    }
}
```

### Converters

The object hierarchy representing a resource is not practical when you want to store or transport the information about the resource. There is a converter that can be used to convert back and forth from simpler string representations:

```csharp
using Bifrost.Applications;
using Events.MyBoundedContext;

namespace MyApplication
{
    public class MySystem 
    {
        public MySystem(
            IApplicationResources applicationResources,
            IApplicationResourceIdentifierConverter converter)
        {
            var identifier = applicationResources.Identify(typeof(MyEvent));

            // Make a string representation
            var identifierAsString = converter.AsString(identifier);

            // Get an identifier from a string representation
            var identifierConvertedBack = converter.FromString(identifierAsString);
        }
    }
}
```

### Resolvers

When you have an identifier and you want to get to the actual runtime type, you need something that is capable of resolving it. In Bifrost for C# you have something that does just that; `IApplicationResourceResolver`:

```csharp
using Bifrost.Applications;
using Events.MyBoundedContext;

namespace MyApplication
{
    public class MySystem 
    {
        public MySystem(
            IApplicationResources applicationResources,
            IApplicationResourceIdentifierConverter converter,
            IApplicationResourceResolver resolver)
        {
            var identifier = applicationResources.Identify(typeof(MyEvent));
            var identifierAsString = converter.AsString(identifier);

            var identifierConvertedBack = converter.FromString(identifierAsString);

            // Get the type back
            var type = resolver.Resolve(identifierConvertedBack);
        }
    }
}
```

## Well known areas

There are areas that Bifrost consider well known and part of every Bifrost application. These are as follows:

| Area     | Description                                                                  |
| -------- | ---------------------------------------------------------------------------- |
| Domain   | Holds your commands, input validation rules, business rules, aggregate roots |
| Events   | Holds only your Events                                                       |
| Read     | Holds your readmodels, queries                                               |
| Frontend | Holds everything related to your frontend                                    |

