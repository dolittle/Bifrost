using Bifrost.Sagas;
using Bifrost.Sagas.Exceptions;
using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_concluding_a_saga_that_is_new : given.a_saga_narrator
    {
        static SagaWithOneChapterProperty saga;
        static IChapter chapter;
        static SagaConclusion conclusion;
        Establish context = () =>
        {
            chapter = new SimpleChapter();
            saga = new SagaWithOneChapterProperty(chapter);
        };

        Because of = () => conclusion = narrator.Conclude(saga);

        It should_have_a_non_successful_conclusion = () => conclusion.Success.ShouldBeFalse();
        It should_have_an_invalid_saga_state_transition_exception = () => conclusion.Exception.ShouldBeOfExactType<InvalidSagaStateTransitionException>();
        It should_not_have_called_the_on_conclude_method = () => saga.OnConcludeCalled.ShouldEqual(0);
        It should_publish_the_exception = () =>
            exception_publisher_mock.Verify(m => m.Publish(Moq.It.IsAny<InvalidSagaStateTransitionException>()));
    }
}