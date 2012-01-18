using Bifrost.Fakes.Events;

namespace Bifrost.Specs.Events.for_ProcessMethodInvoker
{
    public class TypeWithSimpleEventProcessMethod
    {
        public bool ProcessCalled = false;
        public SimpleEvent EventPassed = null;
        public void Process(SimpleEvent @event)
        {
            ProcessCalled = true;
            EventPassed = @event;
        }
    }
}