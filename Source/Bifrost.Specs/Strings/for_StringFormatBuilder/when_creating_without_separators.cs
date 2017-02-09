using System;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_creating_without_separators
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => new StringFormatBuilder());

        It should_throw_missing_separators = () => exception.ShouldBeOfExactType<MissingSeparator>();
    }
}
