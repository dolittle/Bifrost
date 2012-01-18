using System;
using Bifrost.Events;
using Bifrost.Fakes.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
	public class a_saga_with_events_from_multiple_aggregated_roots : a_saga
	{
		protected static Guid first_aggregated_root_id = Guid.NewGuid();
		protected static Guid second_aggregated_root_id = Guid.NewGuid();


		Establish context = () =>
		                    	{
		                    		var firstEventStream = new UncommittedEventStream(first_aggregated_root_id);
		                    		firstEventStream.Append(new SimpleEvent(first_aggregated_root_id));

		                    		var secondEventStream = new UncommittedEventStream(second_aggregated_root_id);
		                    		secondEventStream.Append(new SimpleEvent(second_aggregated_root_id));

		                    		saga.Save(firstEventStream);
		                    		saga.Save(secondEventStream);
		                    	};

	}
}