using System;
using Bifrost.Events;

namespace Bifrost.Mimir.EventViewer
{
    public class ClientEvent : Event
    {
        public ClientEvent() : base(Guid.NewGuid()) { }
    }
}
