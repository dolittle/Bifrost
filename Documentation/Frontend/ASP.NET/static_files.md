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
A strategy is to include your debug version of the Web.config file a way to disable content caching.

```xml
  <system.webServer>
    <httpProtocol>
      <customHeaders>
        <add name="Cache-Control" value="no-cache, no-store" />
        <add name="Pragma" value="no-cache" />
        <add name="Expires" value="-1" />
      </customHeaders>
    </httpProtocol>
    <staticContent>
      <clientCache cacheControlMode="UseMaxAge" cacheControlMaxAge="00:00:01" />
    </staticContent>
  </system.webServer>
```