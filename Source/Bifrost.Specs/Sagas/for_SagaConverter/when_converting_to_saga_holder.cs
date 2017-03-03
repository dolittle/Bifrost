using Bifrost.Testing.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaConverter
{
    public class when_converting_to_saga_holder : given.a_saga_converter_and_a_saga
    {
        static SagaHolder saga_holder;

        Because of = () => saga_holder = saga_converter.ToSagaHolder(saga);

        It should_set_saga_type = () => saga_holder.Type.ShouldEqual(typeof(SagaWithOneChapterProperty).AssemblyQualifiedName);
        It should_set_name = () => saga_holder.Name.ShouldEqual(typeof (SagaWithOneChapterProperty).Name);
        It should_set_partition = () => saga_holder.Partition.ShouldEqual(expected_partition);
        It should_set_the_key = () => saga_holder.Key.ShouldEqual(expected_key);
    }
}
