using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter.given
{
    public class a_saga_converter_and_a_saga_with_one_chapter : a_saga_converter_and_a_saga
    {
        protected static SimpleChapter chapter;

        Establish context = () =>
                                {
                                    chapter = new SimpleChapter();
                                    saga = new SagaWithOneChapterProperty(chapter);
                                };
    }
}