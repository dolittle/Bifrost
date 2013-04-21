using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaState.given
{
    public class a_state_of_begun : a_state_of_new
    {
        Establish context = () => state.TransitionTo(SagaState.BEGUN);
    }
}