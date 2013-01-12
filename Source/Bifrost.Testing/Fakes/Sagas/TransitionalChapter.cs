using Bifrost.Sagas;

namespace Bifrost.Testing.Fakes.Sagas
{
    public class TransitionalChapter : Chapter, ICanTransitionTo<NonTransitionalChapter>
    {
		public string Something { get; set; }
		public int AnInteger { get; set; }

        public bool OnCreatedWasCalled;

        public bool OnTransitionedToWasCalled
        {
            get { return OnTransitionedToCalled > 0; }
        }

        public int OnTransitionedToCalled;

        public override void OnCreated()
        {
            base.OnCreated();
            OnCreatedWasCalled = true;
        }

        public override void OnTransitionedTo()
        {
            base.OnTransitionedTo();
            OnTransitionedToCalled++;
        }    
    }
}