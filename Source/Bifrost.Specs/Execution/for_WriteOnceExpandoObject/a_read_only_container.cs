using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_WriteOnceExpandoObject
{
    [Behaviors]
    public class a_read_only_container
    {
        protected static Exception exception;
        It should_throw_a_read_only_object_exception = () => exception.ShouldBeOfExactType<ReadOnlyObjectException>();
    }
}
