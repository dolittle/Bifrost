---
title: Views
description: About Bifrosts View Engine 
keywords: 
author: einari
---
# Views

Views are represented as HTML files that contains the structure of for instance a particular feature. Views can also represent the composition of two or more views, for instance a landing page for a feature that consists of multiple views.

## Referencing a view

You can bring in a view quite easily into your composition by using the `data-view` attribute:

```html
<div data-view="location/of/view"></div>
```

The views are located according to the current [URI mapper](uri_mapper.md).
The engine will load the view and strip away any `HTML`, `HEAD` or `BODY` tags if they are present and just add the content of the `BODY` tag into the container that pointed to the view.

## Navigation

Deeplinking and navigation is an important aspect of any application.
By using the `data-navigation-frame`, Bifrost will respond to URL changes and resolve it to views that can be put into the container acting as a navigation frame. You can provide a `home` property that will be used if a specific view is not present in address of the page.

```html
<div data-navigation-frame="home: location/of/view"></div>
```

The views are located according to the current [URI mapper](uri_mapper.md).

