---
title: About Http Modules
description: Learn how Bifrost works with HTTP modules
keywords: ASP.NET
author: einari
---

# Http Module

Bifrost knows how to deal with routes coming in. When it is deep linked routes into your application, Bifrost has
a [HttpModule](https://msdn.microsoft.com/en-us/library/bb398986.aspx) that knows how to serve the right file.
Traditionally in [Single-page applications](https://en.wikipedia.org/wiki/Single-page_application), is has been popular to
use of URLs containing a `#` as a route separator holding the deep linked route into the application after the `#`.
With Bifrost, you don't need to - you can have traditional routes and Bifrost will give you back the correct landing
page representing the entrypoint of the single-page application.

All navigation in the application can assume this will work as well, as the JavaScript side of things picks up on this and
does the right thing for the browser. This could mean that it uses the `#` strategy if necessary, which would also work
with this implementation.

The HttpModule that Bifrost has for this is hooked up by the [BootStrapper](../../api/Bifrost.Web.BootStrapper.html) and
is completely transparent to you as a developer.