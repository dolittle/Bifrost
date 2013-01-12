using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaCommandContext
{
    public class when_committing_with_uncommitted_events_available_and_saga_has_a_chapter_property : given.a_saga_command_context_with_aggregated_root_that_has_uncommitted_events
    {
        static SagaWithOneChapterProperty saga;
        static SimpleChapter chapter;

        Establish context = () =>
                                {
                                    saga = new SagaWithOneChapterProperty();
                                    chapter = new SimpleChapter();
                                    saga.AddChapter(chapter);

                                    command_context = new SagaCommandContext(
                                        saga,
                                        command_mock.Object,
                                        execution_context_mock.Object,
                                        event_store_mock.Object,
                                        uncommitted_event_stream_coordinator_mock.Object,
                                        process_method_invoker_mock.Object,
                                        saga_librarian_mock.Object);

                                    command_context.RegisterForTracking(aggregated_root_mock.Object);
                                };

        Because of = () => command_context.Commit();

        It should_try_to_process_the_events_on_the_chapter = () => process_method_invoker_mock.Verify(p => p.TryProcess(chapter, simple_event));

    }
}