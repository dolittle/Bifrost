using System;
using Bifrost.Domain;
using Bifrost.Fakes.Events;

namespace Bifrost.Fakes.Domain
{
    public class StatefulAggregatedRoot : AggregatedRoot
    {
        public string Value { get; set; }
        public bool EventApplied { get; private set; }

        public StatefulAggregatedRoot(Guid id) : base(id)
        {
        }

        void Handle(SimpleEvent simpleEvent)
        {
            EventApplied = true;
            Value = simpleEvent.Id.ToString();
        }


        public bool CommitCalled = false;
        public override void Commit()
        {
            CommitCalled = true;
            base.Commit();
        }

        public bool RollbackCalled = false;
        public override void Rollback()
        {
            RollbackCalled = true;
            base.Rollback();
        }
    }
}