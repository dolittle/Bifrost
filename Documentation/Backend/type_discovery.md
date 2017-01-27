---
title: About type discovery
description: Learn about how type discovery works
keywords: Types
author: einari
---

# Type Discovery

Bifrost is very focused around [convention over configuration](../Articles/convention_over_configuration.md).
In order for Bifrost to be able to do all its conventions, it needs to be able to know about
the types in the system. The type discovery is built on top of the [assembly discovery](assembly_discovery.md).

## IInstancesOf

If you're interested in getting instances of implementations of a specific type. You can
simply take a dependency to `IInstancesOf<TheBaseType>`. The implementation of `IInstancesOf`
uses the type discovering mechanism and builds a convenience layer on top. It also implements
`IEnumerable` and conveniently lets you iterate directly over the instences of a specific type.

Imagine you have an interface

```csharp

public interface ISomething
{
}
```

Then you have a couple of implementations of this interface

```csharp
public class FirstImplementation : ISomething
{
}


public class SecondImplementation : ISomething
{
}
```

You can then, without having to know about the different implementations take a dependency
to `IInstancesOf`.

```csharp
public class MySystem
{
    public MySystem(IInstancesOf<ISomething> allInstances)
    {
        foreach( var instance in allInstances )
        {
            // Do something
        }
    }
}
```

Internally the instances will be created using the IOC container when the implementation
has dependencies.

With this you can much easier adhere to the Open/Closed principle. Not having to open your
software for modification when you want to introduce new functionality.

## IImplementationsOf

> [!Note]
> Coming Soon

Similar as with `IInstancesOf` but only for getting the Types, you can take a dependency
to `IImplementationsOf`and start iterating over it. `IImplementationsOf`implements
an `IEnumerable<Type>` and lets you enumerate the types it discovered that implements the
type you were asking for.

Imagine having the following interface and you want to know about all implementing types.

```csharp
public interface ISomething
{
}
```

Then you have a couple of implementations of this interface

```csharp
public class FirstImplementation : ISomething
{
}


public class SecondImplementation : ISomething
{
}
```

You can then, without having to know about the different implementations take a dependency
to `IInstancesOf`.

```csharp
public class MySystem
{
    public MySystem(IImplementationsOf<ISomething> allTypes)
    {
        foreach( var type in allTypes )
        {
            // Do something
        }
    }
}
```

