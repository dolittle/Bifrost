using System;
using Bifrost.Mapping;
using Machine.Specifications;

namespace Bifrost.Specs.Mapping.for_Maps
{
    public class when_asking_if_has_map_for_known_combination : given.map_for_source_and_target_in_system
    {
        static bool result;

        Because of = () => result = maps.HasFor(typeof(Source), typeof(Target));

        It should_have_a_map = () => result.ShouldBeTrue();
    }
}
