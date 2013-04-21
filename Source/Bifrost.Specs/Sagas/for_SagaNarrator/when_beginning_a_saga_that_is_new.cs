using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
    public class when_beginning_a_saga : given.a_saga_narrator
    {
        static SagaWithOneChapterProperty saga;
        static SimpleChapter simple_chapter;

        Establish context = () =>
                                {
                                    simple_chapter = new SimpleChapter();
                                    container_mock.Setup(c => c.Get<SagaWithOneChapterProperty>()).Returns(new SagaWithOneChapterProperty());
                                    container_mock.Setup(c => c.Get(typeof (SimpleChapter))).Returns(
                                        simple_chapter);
                                };

        Because of = () => saga = narrator.Begin<SagaWithOneChapterProperty>();

        It should_return_a_saga_instance = () => saga.ShouldNotBeNull();
        It should_ensure_the_saga_instance_has_a_state_of_begun = () => saga.IsBegun.ShouldBeTrue();
        It should_have_called_the_on_begin_method_of_the_saga = () => saga.OnBeginCalled.ShouldEqual(1);
        It should_be_of_correct_type = () => saga.ShouldBeOfType<SagaWithOneChapterProperty>();
        It should_set_chapter_property_to_not_be_null = () => saga.Simple.ShouldNotBeNull();
        It should_set_chapter_property_to_the_chapter_instance = () => saga.Simple.ShouldEqual(simple_chapter);
        It should_call_the_on_created_method_of_the_chapter = () => saga.Simple.OnCreatedWasCalled.ShouldBeTrue();
        It should_add_the_chapter_instance_to_the_saga = () => saga.Chapters.ShouldContain(simple_chapter);
    }
}
