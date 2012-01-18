using System;
using Bifrost.Fakes.Sagas;
using Machine.Specifications;
using Bifrost.Sagas;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_setting_the_current_chapter_by_type_and_it_is_not_part_of_saga : given.a_saga_with_a_chapter_property
    {
        static Exception exception;

        Because of = () => exception = Catch.Exception(() => saga.SetCurrentChapter<SimpleChapter>());

        It should_throw_chapter_does_not_exist_exception = () => exception.ShouldBeOfType<ChapterDoesNotExistException>();
    }
}
