using System;
using System.Linq;
using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaLibrarian
{
	public class when_getting_a_saga : given.a_saga_librarian
	{
		static Guid saga_id;
		static SagaHolder saga_holder;

		Establish context = () =>
								{
									saga_id = new Guid();
									saga_holder = new SagaHolder
														{
															Id = saga_id,
															Type = typeof(SagaWithOneChapterProperty).AssemblyQualifiedName,
															SerializedSaga = "{}"
														};
								
									entity_context_mock.Setup(e => e.Entities).Returns(new[] { saga_holder }.AsQueryable());
								};

		Because of = () => librarian.Get(saga_id);

		It should_convert_from_saga_holder_to_saga = () => saga_converter_mock.Verify(s => s.ToSaga(saga_holder));
	}
}
