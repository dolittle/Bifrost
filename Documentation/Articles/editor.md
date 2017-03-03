---
title: About editor config
description: Learn about how your editor should be configured
keywords: Contributing
author: einari
---

# Editor

In the root of the project sits a `.editorconfig` file that governs how your editor should be configured. If your editor does not support it, you need to
set this up manually.

## Default

All text files has this setting by default.

| Property    | Setting   |
| ----------- | --------- |
| End of line | LF (Unix) |
| Indent      | Spaces    |
| Indent size | 4         |

## YAML

For [YAML](https://en.wikipedia.org/wiki/YAML) files, the following properties are overridden.

| Property    | Setting   |
| ----------- | --------- |
| Indent size | 2         |
