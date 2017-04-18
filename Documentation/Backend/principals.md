---
title: About Principals
description: Learn how principals are resolved
keywords: .NET
author: einari
---
# Principals

On the [execution context](./execution_context.md) you'll find a property for the current `ClaimsPrinicpal`.
Bifrost is oppinionated with the choice of chosing `ClaimsPrincipal` over the interface `IPrincipal` in .NET.
The reasoning behind this is that modern applications are associated with claims based authorization. Since
one can represents non claims based principals and its identities with the `ClaimsPrincipal` there really is
no good reason to not base it on this.

## Default principal

By default Bifrost uses the `ClaimsPrincipal.Current` property to resolve. If the [`ClaimsPrincipal.ClaimsPrincipalSelector`
in .NET](https://docs.microsoft.com/en-us/dotnet/api/system.security.claims.claimsprincipal?view=netframework-4.7) returns
null - a `ClaimsPrincipal` with the identity name of `[Anonymous]` is returned.

## Resolve - ICanResolvePrincipal

You can override the default way of resolving a principal by dropping in your own implementation of `ICanResolvePrincipal`.

```csharp
using Bifrost.Security;

public class MyPrincipalResolver : ICanResolvePrincipal
{
    public ClaimsPrincipal Resolve()
    {
        var claimsPrincipal = /* ... resolve the principal ... */

        return claimsPrincipal;
    }
}
```

There can only be one of these implementations. If you have two, an exception will be thrown.