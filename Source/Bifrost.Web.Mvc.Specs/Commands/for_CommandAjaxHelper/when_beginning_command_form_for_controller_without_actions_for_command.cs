using Bifrost.Testing.Fakes.Commands;
using Bifrost.Web.Mvc.Commands;
using Machine.Specifications;
using It = Machine.Specifications.It;
using System;

namespace Bifrost.Web.Mvc.Specs.Commands.for_CommandAjaxHelper
{
    [Subject(typeof(CommandAjaxHelper))]
    public class when_beginning_command_form_for_controller_without_actions_for_command : given.an_ajax_helper
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => ajax_helper.BeginCommandForm<SimpleCommand, ControllerWithoutActionForCommandController>());

        It should_throw_missing_action_exception = () => exception.ShouldBeOfType<MissingActionException>();
    }
}
