using System.Globalization;
using System.Reflection;
using Bifrost.Reflection;
using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;

namespace Bifrost.Client.Specs.Reflection.for_InvocationHandler.given
{
    public class an_invocation
    {
        protected static Mock<IInvocation> invocation_mock;
        protected static Mock<MethodInfo> method_mock;
        protected static string method_name;
        protected static object[] arguments;
        protected static object return_value;
        protected static Mock<Implementation> implementation_mock;
        protected static InvocationHandler<Interface, Implementation> handler;

        Establish context = () =>
        {
            implementation_mock = new Mock<Implementation>();
            handler = new InvocationHandler<Interface, Implementation>(implementation_mock.Object);
            method_mock = new Mock<MethodInfo>();
            method_mock.SetupGet(m => m.Name).Returns(() => method_name);
            
            invocation_mock = new Mock<IInvocation>();
            invocation_mock.SetupGet(i=>i.Method).Returns(method_mock.Object);
            invocation_mock.SetupGet(i => i.Arguments).Returns(() => arguments);

            method_mock.Setup(m =>
                m.Invoke(
                    implementation_mock.Object,
                    Moq.It.IsAny<BindingFlags>(),
                    Moq.It.IsAny<Binder>(),
                    Moq.It.IsAny<object[]>(),
                    Moq.It.IsAny<CultureInfo>())
               ).Returns(() => return_value);
        };

        
    }
}
