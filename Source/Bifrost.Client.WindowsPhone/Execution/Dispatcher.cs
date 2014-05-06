using System;
using Windows.UI.Core;

namespace Bifrost.Execution
{
    public class Dispatcher : IDispatcher
    {
        CoreDispatcher _dispatcher;

        public Dispatcher(CoreDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public bool CheckAccess()
        {
            return _dispatcher.HasThreadAccess;
        }

        public void BeginInvoke(Delegate del, params object[] arguments)
        {
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => del.DynamicInvoke(arguments));
        }

        public void BeginInvoke(Action a)
        {
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => a());
        }
    }
}
