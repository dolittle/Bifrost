using Bifrost.Sagas;
using Bifrost.Testing.Fakes.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter
{
    public class when_converting_to_saga_holder_with_one_chapter_and_it_is_current : given.a_saga_converter_and_a_saga_with_one_chapter_and_it_is_current
    {
        static SagaHolder saga_holder;
        Because of = () => saga_holder = saga_converter.ToSagaHolder(saga);

        It should_set_current_chapter_type = () => saga_holder.CurrentChapterType.ShouldEqual(typeof (SimpleChapter).AssemblyQualifiedName);
        It should_set_the_id = () => saga_holder.Id.ShouldEqual(saga.Id);
        It should_set_the_type = () => saga_holder.Type = typeof (SagaWithOneChapterProperty).AssemblyQualifiedName;
        It should_serialize_saga = () => serializer_mock.Verify(s=>s.ToJson(saga, Moq.It.IsAny<SagaSerializationOptions>()));
    }
}
