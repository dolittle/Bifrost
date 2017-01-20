using System;
using System.Linq;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaLibrarian
{
    public class when_recording_an_existing_saga : given.a_saga_librarian
    {
        static Guid saga_id;
        static SagaHolder persistent_saga;
        static ISaga saga;

        Establish context = () =>
                                {
                                    saga_id = new Guid();
                                    saga = new SagaWithOneChapterProperty {Id = saga_id};
                                    persistent_saga = new SagaHolder
                                                          {
                                                              Id = saga_id,
                                                              Type = typeof (SagaWithOneChapterProperty).AssemblyQualifiedName,
                                                              SerializedSaga = "{}"
                                                          };
                                    entity_context_mock.Setup(e => e.Entities).Returns(new[] {persistent_saga}.AsQueryable());
                                };

        Because of = () => librarian.Catalogue(saga);

        It should_update_existing_saga = () => entity_context_mock.Verify(e => e.Update(persistent_saga));

    }
}