using System;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_WriteOnceExpandoObject
{
    [Subject(typeof(WriteOnceExpandoObject))]
    public class when_removing_a_value_by_key_after_creation : given.a_write_once_expando_object_with_values
    {
        protected static Exception exception;
        Because of = () => exception = Catch.Exception(() => ((WriteOnceExpandoObject)values).Remove(IntegerKey));
        Behaves_like<a_read_only_container> a_read_only_container;
    }
}
