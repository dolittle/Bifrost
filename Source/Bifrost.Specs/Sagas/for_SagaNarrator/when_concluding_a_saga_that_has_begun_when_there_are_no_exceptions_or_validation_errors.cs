using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_concluding_a_saga_that_has_begun_when_there_are_no_exceptions_or_validation_errors : given.a_saga_narrator
    {
        static SagaWithOneChapterProperty saga;
        static IChapter chapter;
        static SagaConclusion conclusion;

        Establish context = () =>
                                {
                                    chapter = new SimpleChapter();
                                    saga = new SagaWithOneChapterProperty(chapter);
                                    saga.Begin();
                                };

        Because of = () => conclusion = narrator.Conclude(saga);

        It should_have_a_successful_conclusion = () => conclusion.Success.ShouldBeTrue();
        It should_be_valid = () => conclusion.Invalid.ShouldBeFalse();
        It should_have_a_status_of_concluded = () => saga.IsConcluded.ShouldBeTrue();
        It should_have_called_the_on_conclude_method = () => saga.OnConcludeCalled.ShouldEqual(1);
    }
}