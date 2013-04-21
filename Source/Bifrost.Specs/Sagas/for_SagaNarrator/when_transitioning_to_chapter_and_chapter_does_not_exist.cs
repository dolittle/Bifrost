using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
	public class when_transitioning_to_chapter_and_chapter_does_not_exist : given.a_saga_narrator
	{
		static Saga saga;
		static TransitionalChapter chapter;

		Establish context = () =>
		                    	{
		                    		saga = new Saga();
		                    		chapter = new TransitionalChapter();

		                    		container_mock.Setup(c => c.Get<TransitionalChapter>()).Returns(chapter);
		                    	};

		Because of = () => narrator.TransitionTo<TransitionalChapter>(saga);

		It should_add_chapter_to_saga = () => saga.Chapters.ShouldContain(chapter);
        It should_call_the_on_transitioned_method_of_the_chapter = () => chapter.OnTransitionedToWasCalled.ShouldBeTrue();
	}
}