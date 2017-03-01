using Bifrost.Entities;
using Bifrost.Events;
using Bifrost.Sagas;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Sagas.for_SagaLibrarian.given
{
	public class a_saga_librarian
	{
		protected static SagaLibrarian librarian;
		protected static Mock<IEntityContext<SagaHolder>> entity_context;
		protected static Mock<ISagaConverter> saga_converter;

		Establish context = () =>
		                    	{
									entity_context = new Mock<IEntityContext<SagaHolder>>();
									saga_converter = new Mock<ISagaConverter>();
		                    		librarian = new SagaLibrarian(entity_context.Object, saga_converter.Object);
		                    	};
	}
}
