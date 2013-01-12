using System;
using Bifrost.Sagas;

namespace Bifrost.Testing.Fakes.Sagas
{
    public class SagaWithOneChapterProperty : Saga
    {
        public int OnBeginCalled { get; private set; }
        public int OnContinueCalled { get; private set; }
        public int OnConcludeCalled { get; private set; }

        public SimpleChapter Simple { get; set; }

	    public SagaWithOneChapterProperty()
	    {
	    }

        public SagaWithOneChapterProperty(params IChapter[] chapters)
        {
            Array.ForEach(chapters, AddChapter);
        }

        public override void OnBegin()
        {
            OnBeginCalled++;
        }

        public override void OnContinue()
        {
            OnContinueCalled++;
        }

        public override void OnConclude()
        {
            OnConcludeCalled++;
        }
    }
}
