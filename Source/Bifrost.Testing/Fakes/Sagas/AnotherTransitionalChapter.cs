using Bifrost.Sagas;

namespace Bifrost.Testing.Fakes.Sagas
{
    public class AnotherTransitionalChapter : Chapter, ICanTransitionTo<NonTransitionalChapter>, ICanTransitionTo<TransitionalChapter>
    {
        public string Something { get; set; }
        public int AnInteger { get; set; }

        public bool OnCreatedWasCalled;
        public bool OnTransitionedToWasCalled;

        public override void OnCreated()
        {
            base.OnCreated();
            OnCreatedWasCalled = true;
        }

        public override void OnTransitionedTo()
        {
            base.OnTransitionedTo();
            OnTransitionedToWasCalled = true;
        }
    }
}