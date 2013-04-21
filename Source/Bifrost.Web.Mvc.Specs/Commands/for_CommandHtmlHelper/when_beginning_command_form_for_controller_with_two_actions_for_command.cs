using System;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Web.Mvc.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.Commands.for_CommandHtmlHelper
{
    [Subject(typeof(CommandHtmlHelper))]
    public class when_beginning_command_form_for_controller_with_two_actions_for_command : given.an_html_helper
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => html_helper.BeginCommandForm<SimpleCommand, ControllerWithTwoActionsForCommandController>());

        It should_throw_ambiguous_action_exception = () => exception.ShouldBeOfType<AmbiguousActionException>();
    }
}
