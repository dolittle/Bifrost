---
title: About Tenants
description: Learn about the tenant system
keywords: .NET
author: einari
---
# Tenants

A tenant is an entity that uses your system. In a multi-tenant solution, this is often seen as the customers
represented as companies of your system. On a tenant you can have associated properties that you wish to make
accessible in a transparent way. Bifrost provides a cross-cutting approach to dealing with tenants.
There are mechanisms in place to populate the details of a tenant as you please.

## ITenant

At the core sits the `ITenant` interface which holds the TenantId and a dynamic object of properties that you
want to associate with every tenant.

## Tenant ID

Import part of a multi-tenant application is the ability to identify the tenant through the unique id that
represents it. This type of identification is often found on claims, but could also be found elsewhere.
By default Bifrost will try to resolve it from the most common claim types in the following order:

* http://schemas.microsoft.com/identity/claims/tenantid
* http://schemas.microsoft.com/identity/claims/identityprovider
* http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid

If not any of these are found, it will return a string with `[UnknownTenant]`.
The tenant id is a [concept of a string](./concepts_and_value_objects.md).

### Resolving Tenant Id

The default behavior can be overridden if you don't want to associate the tenant id from a claim, but for
instance from the incoming URL - for instance the domain or similar.
By putting in an implementation of the `ICanResolveTenantId` interface you get the chance to do so.

```csharp
using Bifrost.Tenancy;

public class MyTenantIdResolver : ICanResolveTenantId
{
    public TenantId Resolve()
    {
        var tenantId = /* ... resolve from something ... */
        return tenantId;
    }
}
```

There can only be one of these implementations. If you have two, an exception will be thrown.


## Populator - ICanPopulateTenant

In Bifrost you'll find an interface called `ICanPopulateTenant`. If you want to augment the dynamic object
associated with every tenant with details, you have to implement this interface. In fact, it is not possible
to add content to the tenants details in arbitrary code, it only allows to be populated from an implementation
of `ICanPopulateTenant`.

```csharp
using Bifrost.Tenancy;

public class MyTenantPopulator : ICanPopulateTenant
{

    public void Populate(ITenant tenant, dynamic details)
    {
        details.SomethingVerySpecific = "A value";
    }
}
```

Later in any code you'll be able to take a dependency in your constructor to the `ITenant`interface as long as
it is being resolved by the [IOC container](./container.md) set up for Bifrost.

```csharp
using Bifrost.Tenancy;

public class MyCode
{
    public MyCode(ITenant tenant)
    {
        var specific = tenant.Details.SomethingVerySpecific;

        // Your code to use it...
    }
}
```

