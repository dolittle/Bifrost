using System;
using System.Windows.Input;
using Bifrost.Interaction;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Client.Specs.Interaction.for_CommandForMethod
{
    public class when_can_execute_property_changes_with_notification
    {
        static ViewModel view_model;
        static ICommand command;
        static bool changed;


        Establish context = () =>
        {
            view_model = new ViewModel();
            command = new CommandForMethod(view_model, "Method", "CanExecute");
            command.CanExecuteChanged += (s,e) => changed = true;
        };

        Because of = () => view_model.CanExecute = true;

        It should_trigger_can_execute_changed = () => changed.ShouldBeTrue();
    }
}
