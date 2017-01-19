using System;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_getting_chapter_by_type_and_it_is_not_in_saga : given.a_saga
    {
        static TransitionalChapter first_chapter;
        static Exception exception;

        Establish context = () =>
                                {
                                    first_chapter = new TransitionalChapter();
                                    saga.AddChapter(first_chapter);
                                };

        Because of = () => exception = Catch.Exception(() => saga.Get<NonTransitionalChapter>());

        It should_throw_chapter_does_not_exist_exception = () => exception.ShouldBeOfExactType<ChapterDoesNotExistException>();
    }
}