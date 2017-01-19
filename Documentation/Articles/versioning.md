---
title: About versioning
description: Learn about how Bifrost is versioned
keywords: General
author: einari
---

# Versioning

Bifrost follows the [Semantic Versioning](https://semver.org) versioning scheme.

Deviation from the standard; due to filesystem limitations, the **+** for build numbers is a **-**.

During building between major versions the versioning pattern is as follows:

`<major>.<minor>.<patch>-<build>`

When approaching end of development for a specific version; alpha version - the pattern is as follows:

`<major>.<minor>.<patch>-alpha-<build>`

For beta releases, the pattern is as follows.

`<major>.<minor>.<patch>-beta-<build>`

When a major, minor or patch is released, the pattern is as follows.

`<major>.<minor>.<patch>`

When a minor version is released, the patch will be 0.
When a major version is released, the minor and patch will be 0.

The build number is never reset and is constantly increased.

## C# - AssemblyInfo

The C# version type used in AssemblyInfo has 4 numbers and will therefor always
follow the following pattern:

`<major>.<minor>.<patch>.<build>`

