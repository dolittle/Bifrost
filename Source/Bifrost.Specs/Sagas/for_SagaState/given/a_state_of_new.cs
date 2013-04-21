using Bifrost.Sagas;
using Bifrost.Specs.Sagas.for_Saga.given;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaState.given
{
    public class a_state_of_new : a_saga
    {
        protected static SagaState state;
        
        Establish context = () => state = new SagaState();
    }
}