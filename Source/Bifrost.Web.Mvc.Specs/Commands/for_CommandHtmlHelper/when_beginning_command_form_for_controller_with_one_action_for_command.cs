using Bifrost.Testing.Fakes.Commands;
using Bifrost.Web.Mvc.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.Commands.for_CommandHtmlHelper
{
    [Subject(typeof(CommandHtmlHelper))]
    public class when_beginning_command_form_for_controller_with_one_action_for_command : given.an_html_helper
    {
        static CommandForm<SimpleCommand> command_form;

        Because of = () => command_form = html_helper.BeginCommandForm<SimpleCommand, ControllerWithOneActionForCommandController>();

        It should_set_correct_action = () => command_form.Action.ShouldEqual("DoStuff");
        It should_set_correct_controller = () => command_form.Controller.ShouldEqual("ControllerWithOneActionForCommand");
    }
}
