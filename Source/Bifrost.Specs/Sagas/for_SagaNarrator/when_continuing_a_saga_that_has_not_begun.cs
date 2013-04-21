using System;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Bifrost.Sagas.Exceptions;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_continuing_a_saga_that_has_not_begun : given.a_saga_narrator
    {
        static Exception exception;

        static Guid saga_id;
        static SagaWithOneChapterProperty saga;

        Establish context = () =>
                                {
                                    saga_id = Guid.NewGuid();
                                    saga = new SagaWithOneChapterProperty();
                                    librarian_mock.Setup(a => a.Get(saga_id)).Returns(saga);
                                };

        Because of = () => exception = Catch.Exception( () => narrator.Continue(saga_id));

        It should_throw_an_invalid_saga_state_transition_exception = () => exception.ShouldBeOfType<InvalidSagaStateTransitionException>();
        It should_not_have_called_the_on_continue_method = () => saga.OnContinueCalled.ShouldEqual(0);
    }
}