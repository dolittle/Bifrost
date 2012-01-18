using System.Collections.Generic;
using Bifrost.Events;
using Bifrost.Sagas;
using Bifrost.Serialization;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter
{
	public class when_converting_a_saga_with_one_event_to_a_saga_holder : given.a_saga_with_one_event
	{
		static SagaHolder saga_holder;

		Because of = () => saga_holder = saga_converter.ToSagaHolder(saga);

		It should_serialize_uncommitted_events = () => serializer_mock.Verify(s=>s.ToJson(Moq.It.IsAny<IEnumerable<EventHolder>>(), Moq.It.IsAny<SerializationOptions>()));
	}
}
