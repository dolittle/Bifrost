using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaState.given
{
    public class a_state_of_concluded : a_state_of_continuing
    {
        Establish context = () => state.TransitionTo(SagaState.CONCLUDED);
    }
}