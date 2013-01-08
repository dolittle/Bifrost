using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Concepts.given
{
    public class concepts
    {
        protected static StringConcept first_string;
        protected static StringConcept second_string;
        protected static StringConcept same_value_as_second_string;
        protected static IntConcept value_as_an_int;
        protected static LongConcept value_as_a_long;

        Establish context = () =>
            {
                first_string = "first";
                second_string = "second";
                same_value_as_second_string = "second";

                value_as_a_long = 1;
                value_as_an_int = 1;
            };

        public class StringConcept : ConceptAs<string>
        {
            public static implicit operator StringConcept(string value)
            {
                return new StringConcept { Value = value };
            }
        }

        public class IntConcept : ConceptAs<int>
        {
            public static implicit operator IntConcept(int value)
            {
                return new IntConcept { Value = value };
            }
        }

        public class LongConcept : ConceptAs<long>
        {
            public static implicit operator LongConcept(long value)
            {
                return new LongConcept { Value = value };
            }
        }
    }
}