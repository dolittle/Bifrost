using System;
using System.ComponentModel;
using Bifrost.Commands;
using Castle.DynamicProxy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Commands.for_CommandForProxies
{
    public class when_getting_for_command : given.all_dependencies
    {
        protected static CommandForProxies command_for_proxies;
        protected static Type[] interfaces_mixed_in;
        protected static ProxyType result;

        Establish context = () => 
        {
            command_for_proxies = new CommandForProxies(proxying_mock.Object, proxy_builder_mock.Object, interceptor_mock.Object);
            proxying_mock.Setup(p => p.BuildInterfaceWithPropertiesFrom(typeof(MyCommand))).Returns(typeof(InterfaceForCommand));
            proxy_builder_mock.Setup(p =>
                p.CreateClassProxyType(
                    typeof(CommandInstanceHolder),
                    Moq.It.IsAny<Type[]>(),
                    Moq.It.IsAny<ProxyGenerationOptions>()
                )
            ).Returns((Type type, Type[] interfaces, ProxyGenerationOptions options) =>
            {
                interfaces_mixed_in = interfaces;
                return typeof(ProxyType);
            });
        };

        Because of = () => result = command_for_proxies.GetFor<MyCommand>() as ProxyType;

        It should_return_an_instance = () => result.ShouldNotBeNull();
        It should_build_interface_with_properties_from_the_command_type = () => proxying_mock.Verify(p => p.BuildInterfaceWithPropertiesFrom(typeof(MyCommand)), Moq.Times.Once());
        It should_mix_in_command_for = () => interfaces_mixed_in.ShouldContain(typeof(ICommandFor<MyCommand>));
        It should_mix_in_interface_for_command_properties = () => interfaces_mixed_in.ShouldContain(typeof(InterfaceForCommand));
        It should_mix_in_system_icommand = () => interfaces_mixed_in.ShouldContain(typeof(System.Windows.Input.ICommand));
        It should_mix_in_notify_data_error_info = () => interfaces_mixed_in.ShouldContain(typeof(INotifyDataErrorInfo));
        It should_mix_in_notify_hold_command_instance = () => interfaces_mixed_in.ShouldContain(typeof(IHoldCommandInstance));
        It should_set_the_command_instance_on_the_proxy_instance = () => result.CommandInstance.ShouldBeOfExactType<MyCommand>();
    }
}
