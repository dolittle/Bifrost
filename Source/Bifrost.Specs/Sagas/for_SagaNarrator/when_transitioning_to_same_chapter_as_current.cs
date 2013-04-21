using System;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_transitioning_to_same_chapter_as_current : given.a_saga_narrator
    {
        static Saga saga;
        static Exception exception;
        static ChapterTransition transition_result;
        static TransitionalChapter chapter;

        Establish context = () =>
                                {
                                    saga = new Saga();
                                    chapter = new TransitionalChapter();
                                    saga.SetCurrentChapter(chapter);
                                };

        Because of = () => exception = Catch.Exception(() => transition_result = narrator.TransitionTo<TransitionalChapter>(saga));

        It should_not_throw_any_exception = () => exception.ShouldBeNull();
        It should_not_record_saga = () => librarian_mock.Verify(a => a.Catalogue(saga), Times.Never());
        It should_not_return_an_invalid_transition = () => transition_result.Success.ShouldBeTrue();
        It should_have_the_correct_chapter_instance_in_the_transition_to = () => transition_result.TransitionedTo.ShouldEqual(chapter);
        It should_call_on_transitioned_to_on_the_chapter = () => chapter.OnTransitionedToWasCalled.ShouldBeTrue();
    }
}