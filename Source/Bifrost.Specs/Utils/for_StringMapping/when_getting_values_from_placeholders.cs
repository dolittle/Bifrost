using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Utils;
using Machine.Specifications;

namespace Bifrost.Specs.Utils.for_StringMapping
{
    public class when_getting_values_from_placeholders
    {
        const string expected_result = "Say/hello/to/mr.potatohead";

        static StringMapping mapping = new StringMapping(
                "{something}/{else}",
                "Say/{else}/to/{something}"
            );

        static dynamic result;

        Because of = () => result = mapping.GetValues("mr.potatohead/hello");

        It should_contain_the_something_placeholder = () => ShouldExtensionMethods.ShouldEqual(result.something, "hello");
    }
}
