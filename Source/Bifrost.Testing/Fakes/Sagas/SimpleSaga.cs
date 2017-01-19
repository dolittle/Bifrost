using Bifrost.Sagas;

namespace Bifrost.Testing.Fakes.Sagas
{
    public class SimpleSaga : Saga
    {
        public string SomeString { get; set; }
        public int SomeInt { get; set; }
        public double SomeDouble { get; set; }
    }
}