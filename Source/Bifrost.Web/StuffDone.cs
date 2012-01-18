using System;
using Bifrost.Events;

namespace Bifrost.Web
{
    public class StuffDone : Event
    {
        public StuffDone(Guid eventSourceId) : base(eventSourceId)
        {
        }

        public string Something { get; set; }
    }
}