using System;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Testing.Fakes.Events;

namespace Bifrost.Specs.Domain.for_AggregateRootRepository
{
    public class SimpleStatefulAggregateRoot : AggregateRoot
    {
        public int SimpleEventHandled;

        public SimpleStatefulAggregateRoot(Guid id) : base(id)
        {
        }

        void On(SimpleEvent @event)
        {
            SimpleEventHandled++;
        }

        public bool ReApplyCalled = false;
        public CommittedEventStream EventStreamApplied;

        public override void ReApply(CommittedEventStream eventStream)
        {
            ReApplyCalled = true;
            EventStreamApplied = eventStream;
            base.ReApply(eventStream);
        }
    }

    public class SimpleStatelessAggregateRoot : AggregateRoot
    {
        public SimpleStatelessAggregateRoot(Guid id)
            : base(id)
        {
        }

        public bool ReApplyCalled = false;
        public CommittedEventStream EventStreamApplied;

        public override void ReApply(CommittedEventStream eventStream)
        {
            ReApplyCalled = true;
            EventStreamApplied = eventStream;
            base.ReApply(eventStream);
        }
    }
}
