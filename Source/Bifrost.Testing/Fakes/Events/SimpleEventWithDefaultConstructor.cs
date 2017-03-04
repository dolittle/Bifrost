using Bifrost.Events;

namespace Bifrost.Testing.Fakes.Events
{
    public class SimpleEventWithDefaultConstructor : IEvent
    {
        public EventSourceId EventSourceId { get; set; }
    }
}