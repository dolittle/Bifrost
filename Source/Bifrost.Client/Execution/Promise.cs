using System;
namespace Bifrost.Execution
{
    public class Promise
    {
        public void Signal(object data)
        {
        }

        public void Error(object data)
        {

        }

        public Promise OnError(Action<Promise, object> callback)
        {
            return this;
        }

        public Promise ContinueWith(Action<Promise, object> callback)
        {
            return this;
        }
    }
}
