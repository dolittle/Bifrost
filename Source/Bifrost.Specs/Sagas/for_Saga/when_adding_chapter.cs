using Bifrost.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_adding_chapter : given.a_saga
    {
        static TransitionalChapter chapter;

        Establish context = () => chapter = new TransitionalChapter();

        Because of = () => saga.AddChapter(chapter);

        It should_contain_chapter = () => saga.Chapters.ShouldContain(chapter);
    }
}
