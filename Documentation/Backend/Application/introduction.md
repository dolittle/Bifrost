---
title: Introduction
description: About Applications
keywords: 
author: einari
---
# Introduction

Bifrost revolves around the concept of an application. An application can then consist of multiple bounded contexts. Rather than building monolithic applications, Bifrost encourages breaking these up into smaller more digestible pieces that live on their own totally decoupled and segregated from other bounded contexts.

A bounded context is a vertical slice that contains the different tiers or areas of your application. Typically these are:

| Area     | Description                                                                  |
| -------- | ---------------------------------------------------------------------------- |
| Domain   | This is where you'd typically have your commands and aggregate roots         |
| Events   | These are used by the domain to represent state changes                      |
| Read     | Read side consumes the events and translates it into read models             |
| Frontend | Frontend is the composition of the bounded context, bringing it all together |

You can read more about the background and what the ideas and vision behind is [here](../../Articles/overview.md).

## Configuration

The application name matters, everything will be relative to the application name.
Any bounded context you have running on its own that belong to the same application,
will then refer to it by its name.

In C#, the configuration is done in the implementation of `ICanConfigure`.

```csharp
public class Configurator : ICanConfigure
{
    public void Configure(IConfigure configure)
    {
        configure
            .Application("Application name", a => {} /* You'd configure the structure here */);
    }
}
```

For further configuration of structure, related to the resources - please read [this](resources.md).