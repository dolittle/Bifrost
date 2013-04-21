using System;
using Bifrost.Views;
using Machine.Specifications;

namespace Bifrost.Specs.Views.for_View
{
    [Subject(Subjects.getting_single_instance)]
    public class when_getting_a_single_instance_of_object_without_id : given.a_view_for<SimpleObjectWithoutId>
    {
        static Exception exception;
        Because of = () => exception = Catch.Exception(() => Repository.GetById(Guid.NewGuid()));

        It should_throw_object_does_not_have_id_exception = () => exception.ShouldBeOfType<ObjectDoesNotHaveIdException>();
    }
}