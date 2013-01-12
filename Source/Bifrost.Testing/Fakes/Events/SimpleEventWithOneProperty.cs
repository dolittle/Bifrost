using System;
using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events
{
    public class SimpleEventWithOneProperty : Event
    {
        public SimpleEventWithOneProperty() : base(Guid.NewGuid()) { }
        public SimpleEventWithOneProperty(Guid guid) : base(guid) {}        

        public string SomeString { get; set; }
    }
}