using System;
using Bifrost.Sagas;
using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_concluding_a_saga_that_has_begun_that_causes_an_exception : given.a_saga_narrator
    {
        static SagaWithOneChapterProperty saga;
        static IChapter chapter;
        static SagaConclusion conclusion;
        static Exception exception;

        Establish context = () =>
        {
            chapter = new SimpleChapter();
            saga = new SagaWithOneChapterProperty(chapter);
            saga.Begin();
            exception = new ArgumentException();
            librarian_mock.Setup(l => l.Close(saga)).Throws(exception);
        };

        Because of = () => conclusion = narrator.Conclude(saga);

        It should_have_a_non_successful_conclusion = () => conclusion.Success.ShouldBeFalse();
        It should_publish_the_exception = () => exception_publisher_mock.Verify(m => m.Publish(exception));
    }
}