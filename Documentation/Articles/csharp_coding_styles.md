# C# Coding Styles

This guide is not a guideline but should be considered the coding standard for the project and
is subject to automated verification during automated builds and also part of codereviews of
pull requests.

The key words “MUST”, “MUST NOT”, “REQUIRED”, “SHALL”, “SHALL NOT”, “SHOULD”, “SHOULD NOT”,
“RECOMMENDED”, “MAY”, and “OPTIONAL” in this document are to be interpreted as described in
[RFC 2119](https://tools.ietf.org/html/rfc2119).

## Patterns & Practices

It is assumed that all code written is adhering to the patterns & practices described in the
[overview](overview.md) article.

## Compactness

## Modifiers

### this

The *this* modifier **SHALL NOT** be used.

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

## Prefixes and postfixes

A very common thing in naming is to include pre/post fixes that describes the technical implementation
or even the pattern that is being used in the implementation. This does not serve as useful information.
Examples of this is `Manager`, `Helper`, `Repository`, `Controller` and more (e.g. `EmployeeRepository`).
You **SHALL NOT** pre or postfix, but rather come up with a name that describes what it is.
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

## Interface naming

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