using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter.given
{
    public class a_saga_converter_and_a_saga_with_one_chapter_and_it_is_current : a_saga_converter_and_a_saga_with_one_chapter
    {
        Establish context = () => saga.SetCurrentChapter(chapter);
    }
}