using System;
using Bifrost.Reflection;
using Machine.Specifications;

namespace Bifrost.Specs.Reflection.for_Proxying
{
    [Ignore("Work in progress")]
    public class when_building_class_with_properties_from_other_type
    {
        static Proxying proxying;
        static Type result; 

        Establish context = () => proxying = new Proxying();

        Because of = () => result = proxying.BuildClassWithPropertiesFrom(typeof(TypeWithProperties));

        It should_not_be_an_interface = () => result.IsInterface.ShouldBeFalse();
        It should_hold_integer_property = () => result.GetProperty("Integer").ShouldNotBeNull();
        It should_hold_string_property = () => result.GetProperty("String").ShouldNotBeNull();
    }
}