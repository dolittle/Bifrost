﻿using System.Linq;
using Bifrost.Strings;
using Machine.Specifications;

namespace Bifrost.Specs.Strings.for_StringFormatBuilder
{
    public class when_building_recursed_fixed_string : given.an_empty_string_format_builder
    {
        const string fixed_string = "Fixed String";
        static IStringFormatBuilder resulting_builder;
        static IStringFormat string_format;

        Establish context = () => resulting_builder = builder.FixedString(fixed_string, f => f.Recurring());

        Because of = () => string_format = resulting_builder.Build();

        It should_hold_a_recursed_segment = () => ((FixedStringSegment) string_format.Segments.First()).Occurrences.ShouldEqual(SegmentOccurrence.Recurring);
    }
}
