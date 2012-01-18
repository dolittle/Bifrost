using Bifrost.Events;
using Bifrost.Specs.Events.for_EventRepository.given;
using Machine.Specifications;

namespace Bifrost.Specs.Events.for_EventRepository
{
    [Subject(Subjects.saving_events)]
    public class when_saving_uncommitted_events : an_event_source_with_10_uncommitted_events_applied
    {
        Because of = () =>
                  {
                      entity_context_mock.Setup(ec => ec.Insert(Moq.It.IsAny<EventHolder>())).AtMost(10);
                      event_repository.Insert(event_source.UncommittedEvents);
                  };

        It should_save_the_uncommitted_events = () => entity_context_mock.VerifyAll();
    }
}