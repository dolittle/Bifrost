using System;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Fakes.Events;

namespace Bifrost.Specs.Domain.for_AggregatedRootRepository
{
	public class SimpleStatefulAggregatedRoot : AggregatedRoot
	{
	    public int SimpleEventHandled;

		public SimpleStatefulAggregatedRoot(Guid id) : base(id)
		{
		}

        void Handle(SimpleEvent @event)
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

    public class SimpleStatelessAggregatedRoot : AggregatedRoot
    {
        public SimpleStatelessAggregatedRoot(Guid id)
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
