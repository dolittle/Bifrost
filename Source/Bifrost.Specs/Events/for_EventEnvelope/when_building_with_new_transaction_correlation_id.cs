using Bifrost.Events;
using Bifrost.Lifecycle;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventEnvelope
{
    public class when_building_with_new_transaction_correlation_id : given.an_event_envelope
    {
        static TransactionCorrelationId new_transaction_correlation_id;
        static IEventEnvelope result;

        Establish context = () => new_transaction_correlation_id = TransactionCorrelationId.New();

        Because of = () => result = event_envelope.WithTransactionCorrelationId(new_transaction_correlation_id);

        It should_be_a_different_instance = () => result.GetHashCode().ShouldNotEqual(event_envelope.GetHashCode());
        It should_hold_the_new_correlation_id = () => result.CorrelationId.ShouldEqual(new_transaction_correlation_id);
    }
}
