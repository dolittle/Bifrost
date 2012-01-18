using Bifrost.Sagas;

namespace Bifrost.Fakes.Sagas
{
    public class SimpleSagaWithConflictingPropertyNames : Saga
    {
        public SomeOtherThing SomeOtherThing { get; set; }
    }

    public class SomeOtherThing
    {
        public string Id { get; set; }
    }
}
