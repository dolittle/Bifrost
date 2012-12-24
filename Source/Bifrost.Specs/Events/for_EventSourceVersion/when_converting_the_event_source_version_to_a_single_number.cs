using System;

using Machine.Specifications;
using Bifrost.Events;

namespace Bifrost.Specs.Events.for_EventSourceVersion
{
    [Subject(typeof (EventSourceVersion))]
    public class when_converting_the_event_source_version_to_a_single_number : given.a_range_of_event_source_versions
    {
        static double low_low_version;
        static double low_high_version;
        static double high_low_version;
        static double high_high_version;

        static double expected_low_low_version;
        static double expected_low_high_version;
        static double expected_high_low_version;
        static double expected_high_high_version;

        Establish context = () =>
                         {
                            expected_low_low_version = 1.0002;
                            expected_low_high_version = 1.02;
                            expected_high_low_version = 100000.0002;
                            expected_high_high_version = 100000.02;
                         };

        Because of = () =>
                         {
                             high_high_version = high_high.Combine();
                             high_low_version = high_low.Combine();
                             low_high_version = low_high.Combine();
                             low_low_version = low_low.Combine();
                         };

        It should_combine_the_version_numbers = () =>
                                                    {
                                                        high_high_version.ShouldEqual(expected_high_high_version);
                                                        high_low_version.ShouldEqual(expected_high_low_version);
                                                        low_high_version.ShouldEqual(expected_low_high_version);
                                                        low_low_version.ShouldEqual(expected_low_low_version);
                                                    };
    }
}