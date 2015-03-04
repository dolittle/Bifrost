using System;
using System.Reflection;
using Bifrost.Reflection;
using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Reflection.for_InvocationHandler
{
    public class when_handling_on_implementation_that_needs_target_instance_but_has_wrong_type
    {
        static InvocationHandler<Interface, ImplementationNeedingTargetInstanceWithWrongType> handler;
        static Mock<ImplementationNeedingTargetInstanceWithWrongType> implementation_mock;
        static Mock<IInvocation> invocation_mock;
        static Mock<Interface> interface_mock;
        static Mock<MethodInfo> method_mock;

        static Exception exception;

        Establish context = () => 
        {
            interface_mock = new Mock<Interface>();
            implementation_mock = new Mock<ImplementationNeedingTargetInstanceWithWrongType>();
            handler = new InvocationHandler<Interface, ImplementationNeedingTargetInstanceWithWrongType>(implementation_mock.Object);
            invocation_mock = new Mock<IInvocation>();
            invocation_mock.Setup(i=>i.Proxy).Returns(interface_mock.Object);
            method_mock = new Mock<MethodInfo>();
            invocation_mock.Setup(i => i.Method).Returns(method_mock.Object);
        };

        Because of = () => exception = Catch.Exception(() => handler.Handle(invocation_mock.Object));

        It should_throw_target_instance_type_mismatch = () => exception.ShouldBeOfExactType<TargetInstanceTypeMismatch>();
    }
}
