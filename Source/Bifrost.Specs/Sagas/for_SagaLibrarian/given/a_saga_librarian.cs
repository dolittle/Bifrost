using Bifrost.Entities;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaLibrarian.given
{
    public class a_saga_librarian
    {
        protected static SagaLibrarian librarian;
        protected static Mock<IEntityContext<SagaHolder>> entity_context_mock;
        protected static Mock<ISagaConverter> saga_converter_mock;

        Establish context = () =>
                                {
                                    entity_context_mock = new Mock<IEntityContext<SagaHolder>>();
                                    saga_converter_mock = new Mock<ISagaConverter>();
                                    librarian = new SagaLibrarian(entity_context_mock.Object, saga_converter_mock.Object);
                                };
    }
}
