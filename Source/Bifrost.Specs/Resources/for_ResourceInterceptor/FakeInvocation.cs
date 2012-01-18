using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Specs.Resources.for_ResourceInterceptor
{
    public class FakeInvocation : IInvocation
    {
        public object[] Arguments { get; set; }

        public Type[] GenericArguments { get; set; }

        public object GetArgumentValue(int index)
        {
            return null;
        }

        public MethodInfo GetConcreteMethod()
        {
            return null;
        }

        public MethodInfo GetConcreteMethodInvocationTarget()
        {
            return null;
        }

        public object InvocationTarget { get; set; }

        public MethodInfo Method { get; set; }
        public MethodInfo MethodInvocationTarget { get; set; }

        public object ReturnValueToSetOnProceed { get; set; }

        public void Proceed()
        {
            ReturnValue = ReturnValueToSetOnProceed;
        }

        public object Proxy { get; set; }
        public object ReturnValue { get; set; }

        public void SetArgumentValue(int index, object value)
        {
        }

        public Type TargetType { get; set; }
    }
}
