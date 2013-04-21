using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaState.given
{
    public class a_state_of_continuing : a_state_of_begun
    {
        Establish context = () => state.TransitionTo(SagaState.CONTINUING);
    }
}