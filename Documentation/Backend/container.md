---
title: About container
description: Describes what the container is and why its needed
keywords: Container
author: einari
---

# Container

Bifrost is using the *dependency principle* and is relying on an external system providing
its dependencies. This is typically an IOC containers job of providing this. In certain
areas of Bifrost, the container is used directly as a service locator. This is in places
where we typically [discover types](type_discovery.md) and need to be able to create
instances of them, in case they have dependencies that needs to be resolved.
