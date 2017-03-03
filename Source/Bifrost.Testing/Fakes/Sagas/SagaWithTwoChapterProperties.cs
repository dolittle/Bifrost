using Bifrost.Sagas;

namespace Bifrost.Testing.Fakes.Sagas
{
    public class SagaWithTwoChapterProperties : Saga
    {
        public SimpleChapter Simple1 { get; set; }
        public SimpleChapter Simple2 { get; set; }
    }
}