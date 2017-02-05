using System;
using Bifrost.Application;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Application.for_BoundedContext
{
    public class when_adding_existing_module
    {
        static BoundedContext bounded_context;
        static Mock<IModule> module_mock;
        static Exception exception;

        Establish context = () =>
        {
            bounded_context = new BoundedContext("Some bounded context");
            module_mock = new Mock<IModule>();
            bounded_context.AddModule(module_mock.Object);
        };

        Because of = () => exception = Catch.Exception(() => bounded_context.AddModule(module_mock.Object));

        It should_throw_module_already_added_to_bounded_context = () => exception.ShouldBeOfExactType<ModuleAlreadyAddedToBoundedContext>();
    }
}
