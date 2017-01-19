using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_getting_chapter_by_type : given.a_saga
    {
        static TransitionalChapter first_chapter;
        static NonTransitionalChapter second_chapter;

        static IChapter fetched_chapter;

        Establish context = () =>
                                {
                                    first_chapter = new TransitionalChapter();
                                    second_chapter = new NonTransitionalChapter();

                                    saga.AddChapter(first_chapter);
                                    saga.AddChapter(second_chapter);
                                };

        Because of = () => fetched_chapter = saga.Get<NonTransitionalChapter>();

        It should_return_a_chapter = () => fetched_chapter.ShouldNotBeNull();
        It should_return_a_chapter_of_correct_type = () => fetched_chapter.ShouldBeOfExactType<NonTransitionalChapter>();
    }
}
