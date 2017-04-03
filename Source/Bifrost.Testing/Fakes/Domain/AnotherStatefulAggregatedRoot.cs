using System;
using Bifrost.Domain;
using Bifrost.Testing.Fakes.Events;

namespace Bifrost.Testing.Fakes.Domain
{
    public class AnotherStatefulAggregatedRoot : AggregateRoot
    {
        public string Value { get; set; }
        public string OneProperty { get; set; }
        public bool EventApplied { get; private set; }
        public bool EventWithOnePropertyApplied { get; private set; }

        public AnotherStatefulAggregatedRoot(Guid id)
            : base(id)
        {
        }

        void On(AnotherSimpleEvent simpleEvent)
        {
            EventApplied = true;
            Value = simpleEvent.Content;
        }

        void On(SimpleEventWithOneProperty simpleEventWithOneProperty)
        {
            EventWithOnePropertyApplied = true;
            OneProperty = simpleEventWithOneProperty.SomeString;
        }
    }
}