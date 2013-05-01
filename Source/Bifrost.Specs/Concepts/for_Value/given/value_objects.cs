using System;
using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.for_Value.given
{
    public class value_objects
    {
        protected static MyValue first_value;
        protected static MyValue identical_values_to_first_value;
        protected static MyValue transposed_values;
        protected static MyValue different_value;
        protected static MyInheritedValue inherited_value_with_identical_values;

        Establish context = () =>
            {
                first_value = new MyValue
                    {
                        FirstIntValue = 1,
                        SecondIntValue = 2,
                        FirstStringValue = "one",
                        SecondStringValue = "two"
                    };

                identical_values_to_first_value = new MyValue
                    {
                        FirstIntValue = first_value.FirstIntValue,
                        SecondIntValue = first_value.SecondIntValue,
                        FirstStringValue = first_value.FirstStringValue,
                        SecondStringValue = first_value.SecondStringValue
                    };

                transposed_values = new MyValue
                {
                    FirstIntValue = first_value.SecondIntValue,
                    SecondIntValue = first_value.FirstIntValue,
                    FirstStringValue = first_value.SecondStringValue,
                    SecondStringValue = first_value.FirstStringValue
                };

                different_value = new MyValue
                    {
                        FirstIntValue = 100,
                        SecondIntValue = 200,
                        FirstStringValue = "First",
                        SecondStringValue = "Second"
                    };

                inherited_value_with_identical_values = new MyInheritedValue
                {
                    FirstIntValue = 1,
                    SecondIntValue = 2,
                    FirstStringValue = "one",
                    SecondStringValue = "two"
                };
            };
    }

    public class MyValue : Value<MyValue>
    {
        public string FirstStringValue { get; set; }
        public string SecondStringValue { get; set; }
        public int FirstIntValue { get; set; }
        public int SecondIntValue { get; set; }
    }

    public class MyInheritedValue : MyValue
    {
        
    }
}