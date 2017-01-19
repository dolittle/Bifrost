using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga_with_a_chapter_property
    {
        protected static SagaWithOneChapterProperty saga;
        protected static SimpleChapter chapter;

        Establish context = () =>
                                {
                                    saga = new SagaWithOneChapterProperty();
                                    chapter = new SimpleChapter();
                                };
    }
}