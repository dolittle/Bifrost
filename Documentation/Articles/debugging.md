---
title: About Debugging
description: Learn how to debug into the Bifrost source code
keywords: .NET
author: einari
---

# Debugging

If you want to debug into the Bifrost source code from Visual Studio you need the symbols downloaded locally
In most situations, you'd typically want to just grab the debug-symbols, step into the code and figure out whatever it is you're interested in.

## Using debug symbols

All official releases of Bifrost after v1.0.0.8 have the symbols available on [SymbolSource](http://www.symbolsource.org).

## Enable source server

To enable symbol server support within Visual Studio do the following:

1. Tools > Options > Debugging > General
   - Check "Enable source server support"
1. Tools > Options > Debugging > Symbols
   - Click the "new"-icon
   - Enter the following server: ``http://srv.symbolsource.org/pdb/Public``

More resources:

- [SymbolSource.org](http://www.symbolsource.org)
- [SymbolSource.org - Configuring Visual Studio](http://www.symbolsource.org/)


## Get the source code

If you may want to get a bit more involved, and actually head over to the [Bifrost GitHub repository](http://github.com/dolittle/Bifrost)
to get a given version of the code. The differnt versions are marked with tags, which should allow you to navigate between releases.