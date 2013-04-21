using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_Saga
{
    [Subject(typeof (Saga))]
    public class when_creating_a_saga : given.a_saga
    {
        It should_have_a_status_of_new = () => saga.IsNew.ShouldBeTrue();
    }
}