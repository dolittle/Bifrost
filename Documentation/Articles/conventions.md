---
title: About conventions
description: Learn about how Bifrost builds on conventions
keywords: General
author: einari
---

# Conventions

## Upper CamelCase vs lower camelCase

All C# code consistently uses upper CamelCase - also called Pascal Case. 
While all JavaScript is consistently using lower camelCase - with the exception of
types that can be instantiated. These have upper CamelCase. This last convention is
a convention that is common in the JavaScript space. 

Going between the two worlds, Bifrost makes sure to translate everything. 
During serialization for instance, translation is done for naming - both ways - making
it feel natural to a C# developer as well as a JavaScript developer.
