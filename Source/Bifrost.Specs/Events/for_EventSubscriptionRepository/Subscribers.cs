using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Testing.Fakes.Events;

namespace Bifrost.Specs.Events.for_EventSubscriptionRepository
{
    public class Subscribers
    {
        public void Process(SimpleEvent @event)
        {
        }

        public void Process(AnotherSimpleEvent @event)
        {
        }
    }
}
