using Bifrost.Events;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_ProcessMethodInvoker.given
{
    public class a_process_method_invoker
    {
        protected static ProcessMethodInvoker invoker;

        Establish context = () => invoker = new ProcessMethodInvoker();
    }
}
