using Bifrost.Events;
using Bifrost.Sagas;
using Bifrost.Validation;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaNarrator.given
{
    public class a_saga_narrator
    {
        protected static SagaNarrator narrator;
        protected static Mock<ISagaLibrarian> librarian_mock;
        protected static Mock<IServiceLocator> service_locator_mock;
        protected static Mock<IChapterValidationService> chapter_validation_service_mock;
    	protected static Mock<IEventStore> event_store_mock;

        Establish context = () =>
                            {
                                librarian_mock = new Mock<ISagaLibrarian>();
                                service_locator_mock = new Mock<IServiceLocator>();
                                chapter_validation_service_mock = new Mock<IChapterValidationService>();
								event_store_mock = new Mock<IEventStore>();

                                narrator = new SagaNarrator(
                                    librarian_mock.Object,
                                    service_locator_mock.Object,
                                    chapter_validation_service_mock.Object,
									event_store_mock.Object
									);

                            };
    }
}
