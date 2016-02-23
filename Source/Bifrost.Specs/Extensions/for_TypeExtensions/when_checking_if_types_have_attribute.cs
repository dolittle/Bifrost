using System;
using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_TypeExtensions
{
    public class when_checking_if_types_have_attributes
    {
        [Custom1]
        interface ITest { }

        [Custom2]
        class Type1 : ITest { }

        class Type2 : Type1 { }

        static Type type_1 = new Type1().GetType();
        static Type type_2 = new Type2().GetType();

        It should_find_attribute_directly_on_class = () => type_1.HasAttribute<Custom2Attribute>().ShouldBeTrue();

        It should_not_search_interfaces_by_default = () => type_2.HasAttribute<Custom1Attribute>().ShouldBeFalse();

        It should_not_search_supertype_by_default = () => type_2.HasAttribute<Custom2Attribute>().ShouldBeFalse();

        It should_search_interfaces_if_asked = () => type_2.HasAttribute<Custom1Attribute>(true).ShouldBeTrue();

        It should_search_supertypes_if_asked = () => type_2.HasAttribute<Custom2Attribute>(true).ShouldBeTrue();
    }
}