using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    public class when_setting_the_current_chapter : given.a_saga_with_a_chapter_property
    {
        Because of = () => saga.SetCurrentChapter(chapter);

        It should_set_the_current_chapter = () => saga.CurrentChapter.ShouldEqual(chapter);
        It should_set_the_chapter_property = () => saga.Simple.ShouldEqual(chapter);
        It should_call_the_on_set_current_hook = () => saga.Simple.OnSetCurrentWasCalled.ShouldBeTrue();
    }
}