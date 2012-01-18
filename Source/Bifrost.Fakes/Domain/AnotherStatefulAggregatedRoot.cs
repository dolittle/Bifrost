using System;
using Bifrost.Domain;
using Bifrost.Fakes.Events;

namespace Bifrost.Fakes.Domain
{
    public class AnotherStatefulAggregatedRoot : AggregatedRoot
    {
        public string Value { get; set; }
        public string OneProperty { get; set; }
        public bool EventApplied { get; private set; }
        public bool EventWithOnePropertyApplied { get; private set; }

        public AnotherStatefulAggregatedRoot(Guid id)
            : base(id)
        {
        }

        void Handle(AnotherSimpleEvent simpleEvent)
        {
            EventApplied = true;
            Value = simpleEvent.Id.ToString();
        }

        void Handle(SimpleEventWithOneProperty simpleEventWithOneProperty)
        {
            EventWithOnePropertyApplied = true;
            OneProperty = simpleEventWithOneProperty.SomeString;
        }
    }
}