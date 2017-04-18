---
title: About Execution Context
description: Learn about how to leverage the Execution Context
keywords: .NET
author: einari
---
# Execution Context

During execution sometimes one needs information that are cross cutting the application.
Such as who is the currently logged in user, current culture, what is the current tenant and other custom meta
data you want is relative to the environment.
In Bifrost this is formalized through something called ``IExecutionContext``.

## Taking it as a dependency

You can take a dependency directly to the ``IExecutionContext`` on the constructor of any types
resolved by the IOC.

```csharp
using Bifrost.Execution;

public class MySystem
{
    public MySystem(IExecutionContext executionContext)
    {
        // Put your code here
    }
}
```

## ExecutionContextManager

If the type taking the dependency has a lifecycle of transient or per web request lifecycle, this is fine.
With a singleton lifecycle, this is not very useful at all, as you would get the execution context
that is valid at the first creation time. Any changes after that would not be reflected.
If you have a singleton, you can take a dependency to something called IExecutionContextManager

```csharp
using Bifrost.Execution;

public class MySystem
{
    public MySystem(IExecutionContextManager executionContextManager)
    {
            var current = executionContextManager.Current;

            // Put your code here
    }
}
```

On the execution context you will find the following properties:

| Property  | Type        |
| --------- | ----------- |
| Principal | IPrincipal  |
| Culture   | CultureInfo |
| System    | string      |
| Tenant    | ITenant     |
| Details   | dynamic     |


### Principal

The principal is populated by whatever resolver is configured for the system to resolve it.
You can read more about principal and how the resolvers work [here](principals.md).

### Culture

Culture is the culture that is currently set on the thread.

### System

System represents a string that just gives a name of your application - the currently running system.
It exists as a property on the configure object of Bifrost, also called System. During configuration
you simply set that property.

### Tenant

Tenant is resolved by the tenancy system. You can read more about this [here](tenants.md).

### Details

The last property is a dynamic property called Details - it can only be populated by placing a class
implementing ICanPopulateExecutionContextDetails anywhere, in fact you can have multiple.
Bifrot will discover all your implementations and call the `Populate()` method whenever the
execution context needs to be populated.

```csharp
using Bifrost.Execution;

public class MyPopulator : ICanPopulateExecutionContextDetails
{
    public Populate(IExecutionContext executionContext, dynamic details)
    {
        details.SomeMetaData = "Hello world";
    }
}
```