using System;
using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_getting_last_committed_version_without_any_aggregate_with_that_id : given.a_saga_and_an_event_source
    {
        static EventSourceVersion version;
 
        Because of = () => version = saga.GetLastCommittedVersionFor(event_source.Object);

        It should_return_a_zero_version = () => version.ShouldEqual(EventSourceVersion.Zero);
    }
}
