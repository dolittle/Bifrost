using System;
using Bifrost.Reflection;
using Machine.Specifications;

namespace Bifrost.Client.Specs.Reflection.for_Proxying
{
    public class when_building_interface_with_properties_from_other_type
    {
        static Proxying proxying;
        static Type result; 

        Establish context = () => proxying = new Proxying();

        Because of = () => result = proxying.BuildInterfaceWithPropertiesFrom(typeof(TypeWithProperties));

        It should_be_an_interface = () => result.IsInterface.ShouldBeTrue();
        It should_hold_integer_property = () => result.GetProperty("Integer").ShouldNotBeNull();
        It should_hold_string_property = () => result.GetProperty("String").ShouldNotBeNull();
    }
}