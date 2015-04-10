using System;

namespace Bifrost.Client.Specs.Reflection.for_InvocationHandler
{
    public abstract class Implementation : Interface
    {
        public abstract string Something { get; set; }

        public abstract string DoSomething(int value, string message);
    }
}
