using System;
using System.Collections.Generic;
using Bifrost.Execution;
using Bifrost.Sagas;
using Bifrost.Serialization;
using Bifrost.Testing.Fakes.Events;
using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaConverter.given
{
	public class a_saga_converter_and_a_saga_holder_with_one_chapter_and_one_event_and_chapter_is_current_and_state_is_continuing
	{
		protected static SagaHolder saga_holder;
		protected static ChapterHolder chapter_holder;
		protected const string expected_string_state = "Something";
		protected const int expected_int_state = 42;
		protected static SimpleEvent simple_event;

		protected static SagaConverter saga_converter;
		protected static Mock<IContainer> container_mock;
		protected static Mock<ISerializer> serializer_mock;

		protected static SagaWithOneChapterProperty expected_saga;
		protected static SimpleChapter expected_chapter;

		Establish context = () =>
		                    	{
									container_mock = new Mock<IContainer>();
									serializer_mock = new Mock<ISerializer>();

		                    		simple_event = new SimpleEvent(Guid.NewGuid());
		                    		saga_holder = new SagaHolder
		                    		              	{
		                    		              		CurrentChapterType = typeof (SimpleChapter).AssemblyQualifiedName,
		                    		              		Type = typeof (SagaWithOneChapterProperty).AssemblyQualifiedName,
		                    		              		SerializedSaga = "{}",
		                    		              		SerializedChapters = "{}",
		                    		              		UncommittedEvents = "{}",
                                                        State = Constants.CONTINUING
		                    		              	};

									chapter_holder = new ChapterHolder
									                 	{
									                 		Type = typeof(SimpleChapter).AssemblyQualifiedName
									                 	};

									expected_chapter = new SimpleChapter();
		                    		expected_saga = new SagaWithOneChapterProperty();

		                    		container_mock.Setup(c => c.Get(typeof (SagaWithOneChapterProperty))).Returns(expected_saga);
									serializer_mock.Setup(s => s.FromJson(typeof(SagaWithOneChapterProperty), Moq.It.IsAny<string>(), Moq.It.IsAny<SagaSerializationOptions>())).Returns(expected_saga);
		                    		serializer_mock.Setup(
		                    			s =>
		                    			s.FromJson(Moq.It.IsAny<List<ChapterHolder>>(), Moq.It.IsAny<string>())).Callback(
		                    			           	(object obj, string json) =>
		                    			           	((List<ChapterHolder>)obj).Add(chapter_holder));
		                    		container_mock.Setup(c => c.Get(typeof (SimpleChapter))).Returns(new SimpleChapter());

									saga_converter = new SagaConverter(container_mock.Object, serializer_mock.Object);
		                    	};

	}
}
