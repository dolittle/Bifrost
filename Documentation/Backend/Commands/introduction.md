---
title: About Commands
description: Learn about Commands and how to leverage them in the frontend
keywords: JavaScript
author: einari, smithmx
---

# Commands

A Command is a typed representation of the user's intent in performing an action.  As such, it is a simple Data Transfer Object and performs
no logic or calculations.  It is an instruction to the system to do something.  

> [!Note]  
> A user does not have to be a person.  Another system can issue commands to our system and is considered a user.

## Naming

A command **SHOULD** be named in the imperative form.  It **SHOULD** also be named in accordance with the intention of the user
rather than the anticipated outcome of the command.  For example, *UpdateShoppingCart* is a poor command name.  It focuses on what will happen,
rather than why the user wants to perform the action.  *AddItemToShoppingCart* better captures what the user wants to achieve, when clicking on 
an "Add to Cart" button.  It is also **recommended** to distinguish between similar actions that have the same main result.  For example, we could
add further commands, even with identical structures and handling, to distinguish between *AddAccessoryToCart*, *AddRecommendationToCart*,
*AddWishlistItemToCart* and so on, where these are or could be important distinctions in our domain. 

## Transaction

A command is a transaction.  There is no concept of a partially successful command.  You **MUST** structure and handle your commands such that 
the command succeeds or fails in its entireity.  A transaction here is a domain concept, not a technical one.  When modelling and naming your commands, 
you should consider the invariants of your domain and the aggregate root that the command will act upon.  

## Structure

A command **MUST** include all necessary information to perform the action.  These **should** be in the form of parameters on the command object.  
You **may** include optional parameters, though it is **recommended** that you create multiple commands that represent the different states associated 
with the optional parameters.  It is **recommended** that you use [Concepts](../concepts_and_value_objects.md) and 
[Value Objects](../concepts_and_value_objects.md) on your commands rather than primitives.  This gives a more expressive command and aids in
validation.

> [!Note]  
> For JavaScript, proxy representations can be used for the commands, read more about the mechanism [here](../../Frontend/JavaScript/proxy_generation.md).

