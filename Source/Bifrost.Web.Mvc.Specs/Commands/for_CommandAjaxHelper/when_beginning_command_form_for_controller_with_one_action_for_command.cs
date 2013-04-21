using Bifrost.Testing.Fakes.Commands;
using Bifrost.Web.Mvc.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.Commands.for_CommandAjaxHelper
{
    [Subject(typeof(CommandAjaxHelper))]
    public class when_beginning_command_form_for_controller_with_one_action_for_command : given.an_ajax_helper
    {
        static CommandForm<SimpleCommand> command_form;

        Because of = () => command_form = ajax_helper.BeginCommandForm<SimpleCommand, ControllerWithOneActionForCommandController>();

        It should_set_correct_action = () => command_form.Action.ShouldEqual("DoStuff");
        It should_set_correct_controller = () => command_form.Controller.ShouldEqual("ControllerWithOneActionForCommand");
    }
}
