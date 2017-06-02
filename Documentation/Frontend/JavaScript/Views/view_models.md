---
title: View Models
description: Learn about View Models
keywords: 
author: einari
---
# View Models

A View Model holds the state and behavior relevant for a [view](views.md). It should not be aware that a view exist, but provide a view agnostic approach to what state and behavior it holds and also aim to not be view centric, but rather let the view deal with the concern of how things are rendered.

The View Model is automatically loaded by [convention](conventions.md), activated and hooked up.

## Lifecycle

All view models are considered transient, meaning that if navigation occurs - the view model instance will go out of scope and be collected.

### onCreated

Your view model can implement a function that gets called when the view model is created. It gets passed the instance of the last descendant in the inheritance chain - your viewModel.

```javascript
Bifrost.namespace("My.Namespace", {
    viewModel: Bifrost.views.ViewModel.extend(function() {
        this.onCreated = function(lastDescendant) {
            // do things
        };
    })
})
```

### onActivated

Your view model can implement a function that gets called when the view model is activated.

```javascript
Bifrost.namespace("My.Namespace", {
    viewModel: Bifrost.views.ViewModel.extend(function() {
        this.onActivated = function() {
            // do things
        };
    })
})
```

### onDeactivated

Your view model can implement a function that gets called when the view model is deactivated and ready for collection.

```javascript
Bifrost.namespace("My.Namespace", {
    viewModel: Bifrost.views.ViewModel.extend(function() {
        this.onDeactivated = function() {
            // do things
        };
    })
})
```