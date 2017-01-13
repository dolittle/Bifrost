---
title: About contributing to documentation
description: Learn about how to contribute to documentation
keywords: Contributing
author: einari
---

# Documentation

Documentation is a key success factor for adoption. This document describes the requirements for documentation.

## API Documentation

All pubic APIs **MUST** be documented.

### C# XML Comments

All C# files **MUST** be documented using the XML documentation as defined [here](https://msdn.microsoft.com/en-us/library/b2s063f7.aspx).
A tutorial can also be found [here](https://msdn.microsoft.com/en-us/library/aa288481(v=vs.71).aspx).

### JavaScript

** WORK IN PROGRESS **

## Markdown

All documentation is written in markdown following the [GitHub flavor](https://help.github.com/categories/writing-on-github/).
Markdown can be written using the simplest of editors (Pico, Nano, Notepad), but there are editors out there that gives
great value and guides you through giving you feedback on errors. Editors like [Visual Studio Code](http://code.visualstudio.com/)
and [Sublime Text](http://sublimetext.com) comes highly recommended.

### Metadata

All files **MUST** have a metadata header at the top of the file following the following format:

```text
---
title: About contributing to documentation
description: Learn about how to contribute to documentation
keywords: Contributing
author: einari (your GitHub accountname)
---
```

## File names

All files should be lower cased. In case of multiple words and concepts that would be in codefiles **CamelCase** you **MUST** seperate
with underscore **_** instead. For instance: [*csharp_coding_styles.md*](chsarp_coding_styles.md).

## DocFX

For processing all the documentation we're using [DocFX](http://dotnet.github.io/docfx/).
It takes the API documentation and all the markdown articles and genereates HTML for us and it gets published.

** More details to come **.