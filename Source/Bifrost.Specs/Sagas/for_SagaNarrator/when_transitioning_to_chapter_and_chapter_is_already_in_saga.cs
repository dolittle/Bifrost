using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_transitioning_to_chapter_and_chapter_is_already_in_saga : given.a_saga_narrator
    {
        static IChapter first_chapter;
        static IChapter second_chapter;
        static SagaWithOneChapterProperty saga;

        Establish context = () =>
                                {
                                    first_chapter = new TransitionalChapter();
                                    second_chapter = new SimpleChapter();
                                    saga = new SagaWithOneChapterProperty(first_chapter, second_chapter);
                                };

        Because of = () => narrator.TransitionTo<TransitionalChapter>(saga);

        It should_go_back_to_first_chapter = () => saga.CurrentChapter.ShouldEqual(first_chapter);
        It should_call_the_on_transitioned_to_on_the_chapter = () => (first_chapter as TransitionalChapter).OnTransitionedToWasCalled.ShouldBeTrue();
    }
}
