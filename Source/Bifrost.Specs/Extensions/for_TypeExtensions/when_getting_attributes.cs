using System;
using System.Linq;
using Bifrost.Extensions;
using Machine.Specifications;

namespace Bifrost.Specs.Extensions.for_TypeExtensions
{
    public class when_getting_attributes
    {
        [Custom1]
        interface ITest { }
        
        [Custom1, Custom2(Data = "Hello")]
        class Type1 : ITest { }

        class Type2 : Type1 { }

        static Type type_1 = new Type1().GetType();
        static Type type_2 = new Type2().GetType();

        It should_find_one_attribute_directly_on_class = () => type_1.GetAttributes<Custom2Attribute>().Count().ShouldEqual(1);

        It should_get_attribute_data = () => type_1.GetAttributes<Custom2Attribute>().First().Data.ShouldEqual("Hello");

        It should_not_search_interfaces_by_default = () => type_2.GetAttributes<Custom1Attribute>().ShouldBeEmpty(); 

        It should_not_search_supertype_by_default = () => type_2.GetAttributes<Custom2Attribute>().ShouldBeEmpty();

        It should_search_interfaces_if_asked = () => type_2.GetAttributes<Custom1Attribute>(true).Count().ShouldEqual(2);

        It should_search_supertypes_if_asked = () => type_2.GetAttributes<Custom2Attribute>(true).Count().ShouldEqual(1);
    }
}