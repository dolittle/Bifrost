---
title: About Static Files
description: Learn how to work effectively
keywords: ASP.NET
author: einari
---
# Static Files

> [!Note]
> Upcoming feature will make this system better, so you don't have to think about this: [#727](https://github.com/dolittle/Bifrost/issues/727)

When doing development, you're not bundling the files in your project and Bifrost will load everything dynamically as it needs
to. When refreshing the browser, content can get cached by the browser and your changes aren't showing up.
One way to deal with this is to turn off caching while in debug mode - not release.

You need to add the following NuGet package: `Microsoft.AspNetCore.StaticFiles`e.g. through the `dotnet cli`:

```cli
dotnet add package Microsoft.AspNetCore.StaticFiles
```

In your `Configure` method of your Startup.cs:

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseStaticFiles(new StaticFileOptions()
    {
        OnPrepareResponse = context =>
        {
            context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
            context.Context.Response.Headers.Add("Expires", "-1");
        }
    });
}
```