---
title: Assets
description: Learn about assets and how they are used
keywords: JavaScript
author: einari
---
# Assets

Files, such as HTML and JavaScript files are discovered from the content root of the Web application and an array of these files is then given to the frontend to be able for instance to know how to resolve files by [convention](Views/conventions.md).

## Route

From the backend a route is established for getting all the assets; `/Bifrost/AssetsManager`. This returns an array of all the assets.
The route supports two queryparameters:

| Parameter | Value                | Description                                            |
| --------- | -------------------- | ------------------------------------------------------ |
| extension | Extension (html, js) | Return only files with given extension                 |
| structure | Extension (html, js) | Return folder structure for files with given extension |

## AssetsManager - Frontend

The frontend has an `assetsManager` type that can be taken as a dependency if you need your code to know about available assets.

```javascript
Bifrost.namespace("Your.Namespace", {
    yourType: Bifrost.Type.extend(function(assetsManager) {
        var scrtips = assetsManager.getScripts();
    })
});
```
