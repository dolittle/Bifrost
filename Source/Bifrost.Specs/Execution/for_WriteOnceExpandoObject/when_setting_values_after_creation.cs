using System;
using Machine.Specifications;
using Bifrost.Execution;

namespace Bifrost.Specs.Execution.for_WriteOnceExpandoObject
{
    [Subject(typeof(WriteOnceExpandoObject))]
    public class when_setting_values_after_creation : given.a_write_once_expando_object_without_values
    {
        protected static Exception exception;
        Because of = () => exception = Catch.Exception(() => values.Something = 5);
        Behaves_like<a_read_only_container> a_read_only_container;
    }
}
