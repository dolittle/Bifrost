using System.Linq;
using Bifrost.Events;
using Bifrost.Specs.Events.for_EventRepository.given;
using Machine.Specifications;
using Bifrost.Fakes.Events;

namespace Bifrost.Specs.Events.for_EventRepository
{
    [Subject(Subjects.saving_events)]
    public class when_saving_uncommitted_events : an_event_source_with_10_uncommitted_events_applied
    {
        static long id;

        Because of = () =>
                  {
                      id = 1;
                      entity_context_mock.Setup(ec => ec.Insert(Moq.It.IsAny<IEvent>())).Callback((IEvent e) => e.Id = id++);
                      event_repository.Insert(event_source.UncommittedEvents);
                  };

        It should_save_the_uncommitted_events = () => id.ShouldEqual(11);
        It should_set_the_ids_from_insert = () => event_source.UncommittedEvents.ToArray()[2].Id.ShouldEqual(3);
    }
}