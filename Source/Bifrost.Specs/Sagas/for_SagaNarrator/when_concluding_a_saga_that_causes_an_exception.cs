using System;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
	public class when_concluding_a_saga_that_has_begun_that_causes_an_exception : given.a_saga_narrator
	{
        static SagaWithOneChapterProperty saga;
		static IChapter chapter;
		static SagaConclusion conclusion;
		static Exception thrown_exception;

		Establish context = () =>
		                    	{
		                    		chapter = new SimpleChapter();
		                    		saga = new SagaWithOneChapterProperty(chapter);
                                    saga.Begin();
		                    		librarian_mock.Setup(l => l.Close(saga)).Throws(new ArgumentException());
		                    	};

		Because of = () => conclusion = narrator.Conclude(saga);

		It should_have_a_non_successful_conclusion = () => conclusion.Success.ShouldBeFalse();
	}
}
