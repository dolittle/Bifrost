# Runtime Exceptions

Exceptions should not be considered a way to do program flow. Exceptions should be treated as an exceptional state of the system
often caused by faulty infrastructure. At times there are exceptions that are valid due to developers not using an API right.
As long as it there is no way to recover an exception is fine. You should not throw an exception and let a caller of your API
deal with the recovery of an exception. Exceptions **MUST** be considered unrecoverable.

Naming of exceptions is covered by the [C# Coding Styles](csharp_coding_styles.md).

## Null

There is no need for **null** in most cases. If you have something that is optional, don't use **null** as a way to check
for wether or not its provided. First of all, be explicit about what your dependencies are. A method should have overloads
without the parameters that are optional. For implementations that are optional, provide a *NullImplementation* as the
default instead. A good example of this is inside Bifrost you have the option of using an
[IEventStore](../../api/Bifrost.Events.IEventStore.html) which then has a default [NullEventStore](../../api/Bifrost.Events.NullEventStore.html)
implementation set as default during configuration. This makes program flow better and no need for dealing with exceptions
such as the [NullReferenceException](https://msdn.microsoft.com/en-us/library/system.nullreferenceexception(v=vs.110).aspx)