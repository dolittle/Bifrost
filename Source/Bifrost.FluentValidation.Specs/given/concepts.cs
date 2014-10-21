using System;
using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.FluentValidation.Specs.Concepts.given
{
    public class concepts
    {
        protected static StringConcept first_string;
        protected static StringConcept second_string;
        protected static StringConcept same_value_as_second_string;
        protected static StringConcept string_is_empty;
        protected static StringConcept string_is_null;
        protected static IntConcept value_as_an_int;
        protected static LongConcept value_as_a_long;
        protected static InheritingFromLongConcept value_as_a_long_inherited;
        protected static InheritingFromLongConcept empty_long_value;


        Establish context = () =>
            {
                first_string = "first";
                second_string = "second";
                same_value_as_second_string = "second";
                string_is_empty = string.Empty;
                string_is_null = new StringConcept();

                value_as_a_long = 1;
                value_as_an_int = 1;
                value_as_a_long_inherited = 1;
                empty_long_value = 0;
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

        public class GuidConcept : ConceptAs<Guid>
        {
            public static implicit operator GuidConcept(Guid value)
            {
                return new GuidConcept { Value = value };
            }
        }

        public class InheritingFromLongConcept : LongConcept
        {
            public static implicit operator InheritingFromLongConcept(long value)
            {
                return new InheritingFromLongConcept { Value = value };
            }
        }

        public class MultiLevelInheritanceConcept : InheritingFromLongConcept
        {
            public static implicit operator MultiLevelInheritanceConcept(long value)
            {
                return new MultiLevelInheritanceConcept { Value = value };
            }
        }
    }
}