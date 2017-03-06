using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter
{
    public class when_converting_a_saga_with_one_event_to_a_saga_holder : given.a_saga_with_one_event
    {
        const string serialized_events = "Some events - kinda serialized.. :) ";
        static SagaHolder saga_holder;

        Establish context = () => serializer.Setup(s => s.ToJson(Moq.It.IsAny<IEnumerable<EventAndEnvelope>>(), null)).Returns(serialized_events);

        Because of = () => saga_holder = saga_converter.ToSagaHolder(saga);

        It should_serialize_uncommitted_events = () => saga_holder.UncommittedEvents.ShouldEqual(serialized_events);
    }
}
