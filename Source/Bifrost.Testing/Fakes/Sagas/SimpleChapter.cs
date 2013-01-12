using Bifrost.Sagas;

namespace Bifrost.Testing.Fakes.Sagas
{
    public class SimpleChapter : Chapter
    {
        public bool OnCreatedWasCalled;
        public bool OnSetCurrentWasCalled;
        public bool OnTransitionedToWasCalled;

        public override void OnCreated()
        {
            base.OnCreated();
            OnCreatedWasCalled = true;
        }

        public override void OnSetCurrent()
        {
            base.OnSetCurrent();
            OnSetCurrentWasCalled = true;
        }

        public override void OnTransitionedTo()
        {
            base.OnTransitionedTo();
            OnTransitionedToWasCalled = true;
        }
    }
}
