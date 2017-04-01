using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventEnvelope
{
    public class when_building_with_new_sequence_number_for_event_type : given.an_event_envelope
    {
        static EventSequenceNumber new_sequence_number;
        static IEventEnvelope result;

        Establish context = () => new_sequence_number = 42L;

        Because of = () => result = event_envelope.WithSequenceNumberForEventType(new_sequence_number);

        It should_be_a_different_instance = () => result.GetHashCode().ShouldNotEqual(event_envelope.GetHashCode());
        It should_hold_the_new_sequence_number = () => result.SequenceNumberForEventType.ShouldEqual(new_sequence_number);
    }
}
