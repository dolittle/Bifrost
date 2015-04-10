using System;
using Bifrost.Reflection;

namespace Bifrost.Client.Specs.Reflection.for_InvocationHandler
{
    public abstract class ImplementationNeedingProxyInstance : Interface, INeedProxyInstance
    {
        public abstract string Something { get; set; }

        public abstract string DoSomething(int value, string message);

        public abstract object Proxy { get; set; }
    }
}
