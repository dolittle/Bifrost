---
title: About getting started
description: Learn about how to get started - building and all
keywords: Building
author: einari
---

# Getting Started

Getting started with Bifrost is very simple.

## Visual Studio

The main Visual Studio solution file that is being used and maintained sits inside the
`Source\Solutions`folder and is called `Bifrost_All.sln`.
It holds all the projects and you should be able to run this directly.
Minimum requirement is Visual Studio 2015.

Remember that if you have turned off automatic restore of NuGet packages in Visual Studio.
You will have to right click the solution in **Solution Explorer** and chose to explicitly
restore packages.

To run the QuickStart once you've got it compiling is simply setting the `Bifrost.QuickStart`
project as the startup project in Visual Studio.

## Visual Studio Code

> [!Note]
> Description coming soon. Some of the developers are using Visual Studio Code running with
> .NET Core. It works fine - description will come later.

## Build Script

In the root folder of the project sits a F# - FAKE build script, this is the one
the build servers are using. If you just want it to build everything and run all the
automated tests, you can pass in the target for doing just that.

On Windows:

```text
build.cmd BuildAndSpecs
```

On Linux / macOS

```text
./build.sh BuildAndSpecs
```

## Running JavaScript specs

The easiest way to run the JavaScript specs is to use Forseti. Open a console / terminal
and navigate into the `./Source` folder.

On Windows, run the following:

```text
..\Tools\Forseti\Forseti.exe
```

On Linux / Mac with Mono installed:

```text
mono ../Tools/Forseti/Forseti.exe
```

This will run all specs, but also start watching the folders for changes and automatically
run the affected specs.