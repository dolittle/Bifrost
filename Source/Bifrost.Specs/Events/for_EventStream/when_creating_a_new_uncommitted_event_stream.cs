using Bifrost.Specs.Events.behaviors;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventStream
{
    public class when_creating_a_new_uncommitted_event_stream : given.an_empty_uncommitted_event_stream
    {
        Behaves_like<an_empty_event_stream> an_event_stream;
    }
}