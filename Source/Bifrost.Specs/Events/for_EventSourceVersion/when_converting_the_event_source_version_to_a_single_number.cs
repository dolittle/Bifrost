using System;

using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSourceVersion
{
    [Subject(typeof (Type))]
    public class when_converting_the_event_source_version_to_a_single_number : given.a_range_of_event_source_versions
    {
        static float low_low_version;
        static float low_high_version;
        static float high_low_version;
        static float high_high_version;

        static float expected_low_low_version;
        static float expected_low_high_version;
        static float expected_high_low_version;
        static float expected_high_high_version;

        Establish context = () =>
                         {
                            expected_low_low_version = 1.0002f;
                            expected_low_high_version = 1.02f;
                            expected_high_low_version = 100000.0002f;
                            expected_high_high_version = 100000.02f;
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