using System;
using Bifrost.Testing.Fakes.Commands;
using Bifrost.Web.Mvc.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Web.Mvc.Specs.Commands.for_CommandAjaxHelper
{
    [Subject(typeof(CommandAjaxHelper))]
    public class when_beginning_command_form_for_controller_with_two_actions_for_command : given.an_ajax_helper
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => ajax_helper.BeginCommandForm<SimpleCommand, ControllerWithTwoActionsForCommandController>());

        It should_throw_ambiguous_action_exception = () => exception.ShouldBeOfType<AmbiguousActionException>();
    }
}
