---
title: About Commands
description: Learn about Commands and how to leverage them in the frontend
keywords: JavaScript
author: einari
---

# Commands

Commands represent the users intent and also the actual transactional boundaries in the system rather than arbitrary technical transactions.
Rather than modelling data, the command represents the action one wants to perform and the necessary
parameters for that action to be performed. The parameters are represented as properties on the command.
One models the commands in such a way that the properties on the command are all required and considered needed in order for the command
to be valid and able to execute.

> [!Note]  
> For JavaScript, proxy representations can be used for the commands, read more about the mechanism [here](../../Frontend/JavaScript/proxy_generation).

