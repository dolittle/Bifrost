# Commands

Commands represent the users intent and also the actual transactional boundaries in the system rather than arbitrary technical transactions.
Rather than modelling data, the command represents the action one wants to perform and the necessary
parameters for that action to be performed. The parameters are represented as properties on the command.
One models the commands in such a way that the properties on the command are all required and considered needed in order for the command
to be valid and able to execute.

A command can also have authorization and validation rules associated with it.

## Proxy generation

For C# developers, there are automatically generated proxies of commands, security and validation related to the command.
These can be taken as dependencies on constructors of types. Read more about [dependency injection](../dependencyInjection.md) in Bifrost.

## Properties

### Setting initial values

### Populating from external source

## States

There are a few properties on a command that can be used programatically and also be bound directly in the HTML.
The state of the properties change as they respond to changes from the user or other parts of the system.

### Authorization

On the client, metadata from the server is brought in telling if a command is authorized for the user 
that is logged in. This 

### isReady & isReadyToExecute & hasChanges

### isValid


### Execution

If a command is not allowed be executed due to validation


## Callbacks

### beforeExecute

### failed

### succeeded

### completed

