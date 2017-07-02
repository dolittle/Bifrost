---
title: About Origins
description: Learn how to configure origins
keywords: Frontend
author: einari
---

# Origins

Bifrost has a relationship with files and artifacts hosted on the server. Typically these are views, viewmodels and API calls
for instance for executing a command or a query.

By default everything will be relative to where the initial document is being loaded.


## Attributes

There are two `data-*` attributes that can be used:

```
data-files-origin
data-apis-origin
```

There are a couple of ways you can use these.
If you want the origin of these to be relative to where you're for instance getting the `/Bifrost/Application` bundle, you
can simply do as follows:

```html
<script src="/Bifrost/Application" data-files-origin data-apis-origin></script>
```

By not setting a specific value, Bifrost will look at the actual `src` of the script tag. This will only work for element types that
has the `src` attribute, typically a `script` tag.

You can be very specific about the origin by setting a value instead:

```html
<script src="/Bifrost/Application" 
        data-files-origin="http://someplace"
		data-apis-origin="http://someotherplace"></script>
```

You can also add these to the body or any element within it:

```html
<body data-files-origin="http://someplace"
	  data-apis-origin="http://someotherplace">
	  
</body>
```


## JavaScript

Another option is to programatically using JavaScript assign the values within a `script`.

```html
<script>
    var configuration = Bifrost.configuration.create();
    configuration.origins.files = "http://someplace";
    configuration.origins.APIs = "http://someotherplace";
</script>
```

