using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga.given
{
    public class a_saga
    {
        protected static Saga saga;

        Establish context = () => saga = new Saga();
    }
}
