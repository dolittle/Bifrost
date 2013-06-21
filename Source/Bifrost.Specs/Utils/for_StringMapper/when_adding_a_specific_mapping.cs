using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Utils;
using Machine.Specifications;

namespace Bifrost.Specs.Utils.for_StringMapper
{
    public class when_adding_a_specific_mapping
    {
        static StringMapper mapper = new StringMapper();
        static IStringMapping mapping = new StringMapping("", "");

        Because of = () => mapper.AddMapping(mapping);

        It should_add_it = () => mapper.Mappings.First().ShouldEqual(mapping);
    }
}
