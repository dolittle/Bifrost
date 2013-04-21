using System;
using Bifrost.Sagas;
using Bifrost.Sagas.Exceptions;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaState
{
    [Subject(typeof (SagaState))]
    public class when_getting_a_state_from_a_string
    {
        static SagaState @new;
        static SagaState continuing;
        static SagaState concluded;
        static SagaState begun;
        static SagaState invalid_state;
        static Exception exception;

        Because of = () =>
                         {
                             @new = Constants.NEW;
                             continuing = Constants.CONTINUING;
                             concluded = Constants.CONCLUDED;
                             begun = Constants.BEGUN;
                             exception = Catch.Exception(() => invalid_state = string.Empty);
                         };

        It should_be_able_to_create_a_new_state = () => @new.IsNew.ShouldBeTrue();
        It should_be_able_to_create_a_begun_state = () => begun.IsBegun.ShouldBeTrue();
        It should_be_able_to_create_a_continuing_state = () => continuing.IsContinuing.ShouldBeTrue();
        It should_be_able_to_create_a_concluded_state = () => concluded.IsConcluded.ShouldBeTrue();
        It should_get_an_unknown_saga_state_exception_when_the_string_is_not_a_valid_state = () => exception.ShouldBeOfType<UnknownSagaStateException>();
    }
}