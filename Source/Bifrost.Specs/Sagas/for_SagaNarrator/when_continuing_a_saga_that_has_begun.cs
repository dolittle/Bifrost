using System;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
	public class when_continuing_a_saga_that_has_begun : given.a_saga_narrator
	{
		static Guid saga_id;
        static SagaWithOneChapterProperty saga;
		static ISaga returned_saga;

		Establish context = () =>
		                    	{
		                    		saga_id = Guid.NewGuid();
                                    saga = new SagaWithOneChapterProperty();
                                    container_mock.Setup(c => c.Get<SagaWithOneChapterProperty>()).Returns(saga);
                                    container_mock.Setup(c => c.Get(typeof(SimpleChapter))).Returns(new SimpleChapter());
                                    saga = narrator.Begin<SagaWithOneChapterProperty>();
                                    librarian_mock.Setup(a => a.Get(saga_id)).Returns(saga);
		                    	};

		Because of = () => returned_saga = narrator.Continue(saga_id);

		It should_return_the_saga = () => returned_saga.ShouldEqual(saga);
	    It should_set_the_saga_state_to_continuing = () => returned_saga.IsContinuing.ShouldBeTrue();
        It should_have_called_the_on_continue_method = () => saga.OnBeginCalled.ShouldEqual(1);
	}
}