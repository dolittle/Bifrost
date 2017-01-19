---
title: About C# coding styles
description: Learn about how to write C# in Bifrost
keywords: Contributing
author: einari
---

# C# Coding Styles

The key words “MUST”, “MUST NOT”, “REQUIRED”, “SHALL”, “SHALL NOT”, “SHOULD”, “SHOULD NOT”,
“RECOMMENDED”, “MAY”, and “OPTIONAL” in this document are to be interpreted as described in
[RFC 2119](https://tools.ietf.org/html/rfc2119).

This is the to be considered the coding standard for Bifrost and is subject to automated
verification during automated builds and also part of codereviews such as those done for
pull requests.

## Patterns & Practices

It is assumed that all code written is adhering to the patterns & practices described in the
[overview](overview.md) article.

## Compactness

In general, code should be compact in the sense that any "noise" of language artifacts or similar
that aren't really needed **SHALL NOT** be used. This to increase readability, not decrease it.
Things that are implicit, **SHALL** be left implicit and not turned into explicits.

## Keywords

### Use of `var`

Types are implicitly provided by the compiler and considered noise during declaration.
If one feel the need for explicitly declaring variables with their type, it is often a
symptom of something else being wrong - such as large methods that you can't get a feel
for straight away. This is most likely breaking the Single Responsibility Pattern.
You **MUST** use `var` and let the compiler infer the type implicitly.

### Private members

In C# the *private* modifier is not needed as this is the default modifier if nothing is specified.
Private members **SHALL NOT** have a private modifier.

Example:

```csharp
public class SomeClass
{
    string _someString;
}
```

### this

Explicit use of *this* **SHALL NOT** be used. With the convention for prefixing private members,
the differentiation is clear.

## Prefixes and postfixes

A very common thing in naming is to include pre/post fixes that describes the technical implementation
or even the pattern that is being used in the implementation. This does not serve as useful information.
Examples of this is `Manager`, `Helper`, `Repository`, `Controller` and more (e.g. `EmployeeRepository`).
You **SHOULD NOT** pre or postfix, but rather come up with a name that describes what it is.
Take `EmployeeRepository`sample, the postfix `Repository` is not useful for the consumer;
a better name would be `Employees`.

## Member variables

Member variables **MUST** be prefixed with an underscore.

Example:

```csharp
public class SomeClass
{
    string _someInstanceMember;
    static string _someStaticMember;
}
```

## One type per file

All files **MUST** contain *only* one type.

## Class naming

Naming of classes **SHALL** be unambiguous and by name tell exactly what it is providing.
Example:

```csharp
// Coordinates uncommitted event streams
public class UncommittedEventStreamCoordinator {}
```

## Interface naming

Its been a common naming strategy to include `I`in front of any `interface`.
Prefixing with `I`can have other meaning as well, such as the actual word "I".
This can give better naming to interfaces and better meaning to names.

Examples:

```csharp
// Implemented by types that can provide configuration
public interface ICanConfigure {}

// Implemented by a type that can provide a container instance
public interface ICanCreateContainer
````

You **SHOULD** try look for this way of naming, as it provides a whole new level
of meaning to the code.

## Private methods

Private methods **MUST** be placed at the end of a class.

Example:

```csharp
public class SomeClass
{
    public void PublicMethod()
    {
        PrivateMethod();
    }


    void PrivateMethod()
    {

    }
}
```

## Exceptions

### flow

Exceptions are to be considered exceptional state. They **MUST NOT** be used to control
program flow. Exceptional state is typically caused by infrastructure problems or other
problems causing normal flow to be able to continue.

### types

You **MUST** create explicit exception types and **NOT** use built in ones.
The exception type can implement one of the standard ones.

Example:

```csharp
public class SomethingIsNull : ArgumentException
{
    public SomethingIsNull() : base("Something was null") {}
}
```

### Throwing

If there is a reason to throw an exception, your validation code and actual throwing
**MUST** be in a separate private method.

Example:

```csharp
public class SomeClass
{
    public void PublicMethod(string something)
    {
        ThrowIfSomethingIsNull(something);
    }

    void ThrowIfSomethingIsNull(string something)
    {
        if( something == null ) throw new SomethingIsNull();
    }
}
```