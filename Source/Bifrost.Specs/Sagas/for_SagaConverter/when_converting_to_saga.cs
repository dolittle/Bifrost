using System.Collections.Generic;
using System.Linq;
using Bifrost.Fakes.Sagas;
using Bifrost.Sagas;
using Bifrost.Serialization;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter
{
	public class when_converting_to_saga : given.a_saga_converter_and_a_saga_holder_with_one_chapter_and_one_event_and_chapter_is_current_and_state_is_continuing
	{
		static ISaga saga;

		Because of = () => saga = saga_converter.ToSaga(saga_holder);

		It should_not_be_null = () => saga.ShouldNotBeNull();
		It should_be_correct_saga_type = () => saga.ShouldBeOfType<SagaWithOneChapterProperty>();
		It should_deserialize_saga = () => serializer_mock.Verify(s => s.FromJson(typeof(SagaWithOneChapterProperty), Moq.It.IsAny<string>(), Moq.It.IsAny<SerializationOptions>()));
		It should_deserialize_chapter = () => serializer_mock.Verify(s => s.FromJson(Moq.It.IsAny<List<ChapterHolder>>(), Moq.It.IsAny<string>(), Moq.It.IsAny<SerializationOptions>()));
		It should_have_one_chapter_in_saga = () => saga.Chapters.Count().ShouldEqual(1);
		It should_have_a_simple_chapter = () => saga.Chapters.First().ShouldBeOfType<SimpleChapter>();
		It should_set_current_chapter = () => saga.CurrentChapter.ShouldNotBeNull();
		It should_set_current_chapter_to_simple_chapter = () => saga.CurrentChapter.ShouldBeOfType<SimpleChapter>();
	    It should_be_in_a_state_of_continuing = () => saga.IsContinuing.ShouldBeTrue();
	}
}