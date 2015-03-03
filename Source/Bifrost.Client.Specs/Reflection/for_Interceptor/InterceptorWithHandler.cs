using Bifrost.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Client.Specs.Reflection.for_Interceptor
{
    public class InterceptorWithHandler : Interceptor
    {
        public InterceptorWithHandler(ICanHandleInvocations handler)
        {
            AddHandler(handler);
        }



        public bool OnInterceptCalled = false;
        public IInvocation invocation_passed_to_intercept;

        public override void OnIntercept(IInvocation invocation)
        {
            invocation_passed_to_intercept = invocation;
            OnInterceptCalled = true;
            base.OnIntercept(invocation);
        }
    }
}
