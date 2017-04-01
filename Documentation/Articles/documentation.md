---
title: About contributing to documentation
description: Learn about how to contribute to documentation
keywords: Contributing
author: einari
---

# Documentation

The key words “MUST”, “MUST NOT”, “REQUIRED”, “SHALL”, “SHALL NOT”, “SHOULD”, “SHOULD NOT”,
“RECOMMENDED”, “MAY”, and “OPTIONAL” in this document are to be interpreted as described in
[RFC 2119](https://tools.ietf.org/html/rfc2119).

Documentation is a key success factor for adoption. This document describes the requirements for documentation.

## API Documentation

All pubic APIs **MUST** be documented.

### C# XML Comments

All C# files **MUST** be documented using the XML documentation as defined [here](https://msdn.microsoft.com/en-us/library/b2s063f7.aspx).
A tutorial can also be found [here](https://msdn.microsoft.com/en-us/library/aa288481(v=vs.71).aspx).

For inheritance in documentation, you can use the [`<ineritdoc/>`](https://ewsoftware.github.io/XMLCommentsGuide/html/86453FFB-B978-4A2A-9EB5-70E118CA8073.htm).

> [!Note]
> This is relying on support in [DocFX](http://dotnet.github.io/docfx/).
> Which seems to be [coming](https://github.com/dotnet/docfx/pull/1178)

### JavaScript

** WORK IN PROGRESS **

## Markdown

All documentation is written in markdown following the [GitHub flavor](https://help.github.com/categories/writing-on-github/).
Markdown can be written using the simplest of editors (Pico, Nano, Notepad), but there are editors out there that gives
great value and guides you through giving you feedback on errors. Editors like [Visual Studio Code](http://code.visualstudio.com/)
and [Sublime Text](http://sublimetext.com) comes highly recommended.

Since the documentation is built by DocFX, you should read up on the markdown supported by it and extensions it supports [here](https://dotnet.github.io/docfx/spec/docfx_flavored_markdown.html).


### Highlighting - notes

Sometimes you need to highlight something with a note. You **MUST** use the following type:

```markdown
> [!Note]
> Notes can have [links](https://github.com/dolittle/bifrost) and **formatting**
```

this results in:

> [!Note]
> Notes can have [links](https://github.com/dolittle/bifrost) and **formatting**


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

Some of this metadata gets put into the generated HTML file and some of it is used for indexing and
other purposes and for future expansion.

## File names

All files should be lower cased. In case of multiple words and concepts that would be in codefiles **CamelCase** you **MUST** seperate
with underscore **_** instead. For instance: [*csharp_coding_styles.md*](chsarp_coding_styles.md).

## DocFX

For processing all the documentation we're using [DocFX](http://dotnet.github.io/docfx/).
It takes the API documentation and all the markdown articles and genereates HTML for us and it gets published during
the continuous build running.


** More details to come **.