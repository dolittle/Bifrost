using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_checking_if_a_chapter_is_in_saga_and_chapter_is_in_saga : given.a_saga
    {
        static TransitionalChapter first_chapter;
        static NonTransitionalChapter second_chapter;

        static bool contains;

        Establish context = () =>
                                {
                                    first_chapter = new TransitionalChapter();
                                    second_chapter = new NonTransitionalChapter();

                                    saga.AddChapter(first_chapter);
                                    saga.AddChapter(second_chapter);
                                };

        Because of = () => contains = saga.Contains<NonTransitionalChapter>();

        It should_return_true = () => contains.ShouldBeTrue();
    }
}