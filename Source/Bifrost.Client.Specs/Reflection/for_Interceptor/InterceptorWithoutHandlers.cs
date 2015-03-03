using Bifrost.Reflection;
using Castle.DynamicProxy;

namespace Bifrost.Client.Specs.Reflection.for_Interceptor
{
    public class InterceptorWithoutHandlers : Interceptor
    {
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
