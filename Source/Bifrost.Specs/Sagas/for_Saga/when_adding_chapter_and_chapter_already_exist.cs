using System;
using Bifrost.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_adding_chapter_and_chapter_already_exist : given.a_saga
    {
        static TransitionalChapter first_chapter;
        static TransitionalChapter second_chapter;
        static Exception exception;

        Establish context = () =>
                                {
                                    first_chapter = new TransitionalChapter();
                                    second_chapter = new TransitionalChapter();
                                };

        Because of = () => exception = Catch.Exception(() =>
                                                           {
                                                               saga.AddChapter(first_chapter);
                                                               saga.AddChapter(second_chapter);
                                                           });

        It should_throw_chapter_already_exist_exception = () => exception.ShouldBeOfType<ChapterAlreadyExistException>();

    }
}