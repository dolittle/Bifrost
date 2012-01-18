using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSourceVersion
{
    [Subject(typeof (EventSourceVersion))]
    public class when_comparing_event_source_versions: given.a_range_of_event_source_versions
    {
        static int low_low_compared_to_low_high;
        static int low_high_compared_to_low_low;
        static int high_low_compared_to_low_high;
        static int low_high_compared_to_high_low;
        static int high_high_compared_to_high_low;
        static int high_low_compared_to_high_high;
        static int low_low_compared_to_low_low;

        const int COMES_BEFORE = -1;
        const int SAME = 0;
        const int COMES_AFTER = 1;

        Because of = () =>
                         {
                            low_low_compared_to_low_high = low_low.CompareTo(low_high);
                            low_high_compared_to_low_low = low_high.CompareTo(low_low);
                            high_low_compared_to_low_high = high_low.CompareTo(low_high);
                            low_high_compared_to_high_low = low_high.CompareTo(high_low);
                            high_high_compared_to_high_low = high_high.CompareTo(high_low);
                            high_low_compared_to_high_high = high_low.CompareTo(high_high);
                            low_low_compared_to_low_low = low_low.CompareTo(low_low);
                         };

        It should_have_low_low_coming_before_low_high = () => low_low_compared_to_low_high.ShouldEqual(COMES_BEFORE);
        It should_have_low_high_coming_after_low_low = () => low_high_compared_to_low_low.ShouldEqual(COMES_AFTER);
        It should_have_high_low_coming_after_low_high = () => high_low_compared_to_low_high.ShouldEqual(COMES_AFTER);
        It should_have_low_high_coming_before_high_low = () => low_high_compared_to_high_low.ShouldEqual(COMES_BEFORE);
        It should_have_high_high_coming_after_high_low = () => high_high_compared_to_high_low.ShouldEqual(COMES_AFTER);
        It should_have_high_low_coming_before_high_high = () => high_low_compared_to_high_high.ShouldEqual(COMES_BEFORE);
        It should_have_low_low_the_same_as_low_low = () => low_low_compared_to_low_low.ShouldEqual(SAME);
    }
}