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

A command can also have authorization and validation rules associated with it.

## Proxy generation

For C# developers, there are automatically generated proxies of commands, security and validation related to the command.
Read more about the proxy generation features of Bifrost [here](proxy_generation.md).
These can be taken as dependencies on constructors of types. Read more about [dependency injection](dependency_injection.md) in Bifrost.

Commands can be created manually in the client code if one wants to. It is not required to go through proxy generation.

## Authorization

On the client, metadata from the server is brought in telling if a command is authorized for the user
that is authenticated. The authorization is applied on the server side using security descriptors.

### Relevant properties

| Property     | Type    | Observable | Description                                         |
| ------------ | ------- | ---------- | --------------------------------------------------- |
| isAuthorized | Boolean | X          | Returns true if command is authorized, false if not |

## Validation

As with authorization, commands can hold metadata from validators, but it will only hold for those rules that has a client side implementation of it.

### Properties

| Property          | Type     | Observable | Description                                                                  |
| ----------------- | -------- | ---------- | ---------------------------------------------------------------------------- |
| isValid           | Boolean  | X          | Returns true if command is valid, false if not                               |
| validtionMessages | string[] | X          | Contains an array of all validation messages for validators that have failed |

## Properties, populating and tracking changes

The properties on a command is also part of the proxy generation.

By default all properties on a command once generated through the proxy generation will hold a default value.
In many cases you want to relate a command to data coming from an existing [ReadModel](../read_model.md) or other source.
During initialization of a [ViewModel](../Views/view_models.md) you would typically connect the command with an external source
for automatic population the command with an initial state.

This initial population of state allows for tracking any changes the user does and can be very useful for the user interface.
You will also find that this information is propagated into the [region](../regions.md) in which the command is being used and also
then available to any parent region up the chain.

### Methods

### properties

| Property         | Type    | Observable | Description                                                    |
| ---------------- | ------- | ---------- | -------------------------------------------------------------- |
| hasChanges       | Boolean | X          | Returns true if command has changes, false if not              |
| isReady          | Boolean | X          | Returns true if command has its properties ready, false if not |
| isReadyToExecute | Boolean | X          | Returns true if command is ready to execute, false if not      |

## Execution

If any of the previously described conditions are not met, the command is not allowed to execute.
Code and UI can access this information and use it to reflect this to user for instance.

### properties

| Property     | Type    | Observable | Description                                       |
| ------------ | ------- | ---------- | ------------------------------------------------- |
| canExecute   | Boolean | X          | Returns true if command can execute, false if not |


## Callbacks

### beforeExecute

### failed

### succeeded

### completed

## Advanced

## Samples