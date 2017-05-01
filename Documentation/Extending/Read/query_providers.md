---
title: Query Providers
description: About Query Providers
keywords: 
author: einari
---
# Query Providers

The `IQueryFor<>`interface that a query implements is just a marker interface.
Meaning that there is really nothing to implement. Instead of having a fixed interface there is a convention in place. Bifrost will look for a property called Query that has a public getter. From this it looks at the returntype and discovers from types loaded if there is a `IQueryProvider`implementation for that return type.

The purpose of the `IQueryProvider`is to provide the capability to deal with the cross cutting aspects of paging for instance. This is not something we want to build into the queries we create, as it is a frontend concern and something we can provide as metadata from the frontend when performing a query. Out of the box comes support for `IQueryable` - both generic and non-generic.

## Implementing

Implementing a provider for your return type is fairly straight forward.
All you need to do is implement the `IQueryProviderFor<YourReturnType>`.
The return type is typically the base non-generic type and Bifrost will understand if you return a specific generically typed version of that - as long as it points to the base type.

The implementation needs then to take care of providing the count of elements and if paging is enabled in the `PagingInfo`metadata, just do the right thing for the type coming in.

Below is a very simple implementation of a provider for IQueryable - in fact, pretty much the same that sits inside Bifrost.

```csharp
public class QueryableProvider : IQueryProviderFor<IQueryable>
{
    public QueryProviderResult Execute(IQueryable query, PagingInfo paging)
    {
        var result = new QueryProviderResult();
        result.TotalItems = query.Count();
        if (paging.Enabled)
        {
            var start = paging.Size * paging.Number;
            query = query.Skip(start).Take(paging.Size);
        }
        result.Items = query;
        return result;
    }
}
```