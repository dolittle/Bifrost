using System;
using Bifrost.Execution;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Execution.for_ApplicationManager
{
    public class when_getting_an_application_and_there_are_multiple_defined : given.an_application_manager
    {
        static Exception exception_thrown;

        Establish context = () => type_discoverer_mock.Setup(t => t.FindSingle<IApplication>()).Throws<MultipleTypesFoundException>();

        Because of = () => exception_thrown = Catch.Exception(() => application_manager.Get());

        It should_throw_multiple_applications_defined_exception = () => exception_thrown.ShouldBeOfType<MultipleApplicationsFoundException>();
    }
}
