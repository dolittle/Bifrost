﻿using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_checking_if_a_chapter_is_in_saga_and_chapter_is_not_in_saga : given.a_saga
    {
        static TransitionalChapter first_chapter;
        static bool contains;

        Establish context = () =>
                                {
                                    first_chapter = new TransitionalChapter();

                                    saga.AddChapter(first_chapter);
                                };

        Because of = () => contains = saga.Contains<NonTransitionalChapter>();

        It should_return_false = () => contains.ShouldBeFalse();
    }
}