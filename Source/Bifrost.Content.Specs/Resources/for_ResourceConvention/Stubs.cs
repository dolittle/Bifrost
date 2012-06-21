using Bifrost.Content.Resources;

namespace Bifrost.Specs.Resources.for_ResourceConvention
{
	public class ResourceInterceptorStub : ResourceInterceptor
	{
		public bool InterceptCalled;

        public ResourceInterceptorStub(IResourceResolver resolver)
            : base(resolver)
        {
            
        }

		public override void Intercept(Castle.DynamicProxy.IInvocation invocation)
		{
			InterceptCalled = true;
			base.Intercept(invocation);
		}
	}

	public class MyResources : IHaveResources
	{
		public virtual string Something { get; set; }
	}

	public class MyStringsNotImplementingStrings
	{
	}


}
