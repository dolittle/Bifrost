using System.ComponentModel;
using Bifrost.Commands;
using Bifrost.Reflection;
using Machine.Specifications;
using Moq;

namespace Bifrost.Client.Specs.Commands.for_CommandForProxyInterceptor.given
{
    public class all_dependencies
    {
        protected static Mock<ICanHandleInvocationsFor<System.Windows.Input.ICommand, CommandInvocationHandler>> command_invocation_mock;
        protected static Mock<ICanHandleInvocationsFor<INotifyDataErrorInfo, CommandNotifyDataErrorInfoHandler>> command_notify_data_error_info_mock;
        protected static Mock<ICanHandleInvocationsFor<ICommandProcess, CommandProcessHandler>> command_process_handler_mock;

        Establish context = () =>
        {
            command_invocation_mock = new Mock<ICanHandleInvocationsFor<System.Windows.Input.ICommand, CommandInvocationHandler>>();
            command_notify_data_error_info_mock = new Mock<ICanHandleInvocationsFor<INotifyDataErrorInfo, CommandNotifyDataErrorInfoHandler>>();
            command_process_handler_mock = new Mock<ICanHandleInvocationsFor<ICommandProcess, CommandProcessHandler>>();
        };
    }
}
