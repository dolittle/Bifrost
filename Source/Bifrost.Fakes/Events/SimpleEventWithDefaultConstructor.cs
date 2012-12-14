using System;
using Bifrost.Events;

namespace Bifrost.Fakes.Events
{
    public class SimpleEventWithDefaultConstructor : IEvent
    {
        public long Id { get; set; }
        public string Name { get; set; }
    	public string CommandName { get; set; }
        public Guid EventSourceId { get; set; }
        public string AggregatedRoot { get; set; }
    	public string EventSourceName { get; set; }
    	public EventSourceVersion Version { get; set; }
        public string CausedBy { get; set; }
        public string Origin { get; set; }
    	public DateTime Occured { get; set; }
        public Guid CommandContext { get; set; }
    }
}