using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventSourceVersion.given
{
    public class a_range_of_event_source_versions
    {
        public const int low_commit = 1;
        public const int high_commit = 100000;
        public const int low_sequence = 2;
        public const int high_sequence = 200;

        protected static EventSourceVersion low_low;
        protected static EventSourceVersion low_high;
        protected static EventSourceVersion high_low;
        protected static EventSourceVersion high_high;

        Establish context = () =>
                                {
                                    low_high = new EventSourceVersion(low_commit, high_sequence);
                                    low_low = new EventSourceVersion(low_commit, low_sequence);
                                    high_low = new EventSourceVersion(high_commit, low_sequence);
                                    high_high = new EventSourceVersion(high_commit, high_sequence);
                                };
    }
}