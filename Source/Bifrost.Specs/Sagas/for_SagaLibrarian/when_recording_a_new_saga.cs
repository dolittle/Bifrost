using System;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaLibrarian
{
    public class when_recording_a_new_saga : given.a_saga_librarian
    {
        static Guid saga_id;
        static SagaWithOneChapterProperty saga;

        Establish context = () =>
                                {
                                    saga_id = new Guid();
                                    saga = new SagaWithOneChapterProperty{Id = saga_id};
                                };

        Because of = () => librarian.Catalogue(saga);

        It should_insert_saga = () => entity_context_mock.Verify(e => e.Insert(Moq.It.IsAny<SagaHolder>()));
    }
}