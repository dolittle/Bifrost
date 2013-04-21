using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_transitioning_to_a_new_chapter_and_the_current_chapter_is_invalid : given.a_saga_narrator
    {
        static Saga saga;
        static IChapter current_chapter;
        static ChapterTransition chapter_transition;

        Establish context = () =>
        {
            current_chapter = new TransitionalChapter();
            saga = new Saga();
            saga.SetCurrentChapter(current_chapter);

            chapter_validation_service_mock.Setup(v => v.Validate(current_chapter)).Returns(new List<ValidationResult>() { new ValidationResult("Test") });
        };

        Because of = () => chapter_transition = narrator.TransitionTo<AnotherTransitionalChapter>(saga);

        It should_not_record_saga = () => librarian_mock.Verify(a => a.Catalogue(saga), Times.Never());
        It should_return_an_invalid_result = () => chapter_transition.Invalid.ShouldBeTrue();
        It should_not_transition_the_chapter = () => saga.CurrentChapter.ShouldEqual(current_chapter);
    }
}