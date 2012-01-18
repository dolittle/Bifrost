using System;
using Bifrost.Execution;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ApplicationManager
{
    public class when_getting_an_application_and_there_is_no_application : given.an_application_manager
    {
        static Exception exception_thrown;

        Establish context = () => type_discoverer_mock.Setup(t => t.FindSingle<IApplication>()).Returns((Type)null);

        Because of = () => exception_thrown = Catch.Exception(() => application_manager.Get());

        It should_throw_application_not_found_exception = () => exception_thrown.ShouldBeOfType<ApplicationNotFoundException>();
    }
}