using System;

namespace Bifrost.Oracle.Events
{
    public class EventDto
    {
        public long Id { get; set; }
        public Guid CommandContext { get; set; }
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public Guid EventSourceId { get; set; }
        public string EventSource { get; set; }
        public int Generation { get; set; }
        public string Data { get; set; }
        public string CausedBy { get; set; }
        public string Origin { get; set; }
        public DateTime Occurred { get; set; }
        public double Version { get; set; }
    }
}