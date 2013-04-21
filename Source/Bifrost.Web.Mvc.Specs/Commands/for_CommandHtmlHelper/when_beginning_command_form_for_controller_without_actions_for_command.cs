using Bifrost.Testing.Fakes.Commands;
using Bifrost.Web.Mvc.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;
using System;

namespace Bifrost.Web.Mvc.Specs.Commands.for_CommandHtmlHelper
{
    [Subject(typeof(CommandHtmlHelper))]
    public class when_beginning_command_form_for_controller_without_actions_for_command : given.an_html_helper
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => html_helper.BeginCommandForm<SimpleCommand, ControllerWithoutActionForCommandController>());

        It should_throw_missing_action_exception = () => exception.ShouldBeOfType<MissingActionException>();
    }
}
