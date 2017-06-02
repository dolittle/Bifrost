---
title: Conventions
description: Learn about conventions for views
keywords: JavaScript
author: einari
---
# Conventions

## View + View Models

Views and View Models are by paired by their filename. When a view is requested, it will ask the [assets manager](assets.md) if there is a view model with the same name but with a .JS extension rather than .HTML. The view model has to be in the correct namespace as described [here](../namespacing.md).
