using Bifrost.Sagas;

namespace Bifrost.Testing.Fakes.Sagas
{
    public class SimpleSagaWithConflictingPropertyNames : Saga
    {
        public SomeOtherThing SomeOtherThing { get; set; }
    }
}
