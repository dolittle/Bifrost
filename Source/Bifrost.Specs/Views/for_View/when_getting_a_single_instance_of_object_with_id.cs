using System.Linq;
using Machine.Specifications;

namespace Bifrost.Specs.Views.for_View
{
    [Subject(Subjects.getting_single_instance)]
    public class when_getting_a_single_instance_of_object_with_id : given.a_view_for<SimpleObjectWithId>
    {
        static SimpleObjectWithId first_object;
        static SimpleObjectWithId second_object;
        static SimpleObjectWithId third_object;

        static SimpleObjectWithId object_got;


        Establish context = () =>
                                {
                                    first_object = new SimpleObjectWithId();
                                    second_object = new SimpleObjectWithId();
                                    third_object = new SimpleObjectWithId();
                                    EntityContextMock.Setup(e => e.GetById(second_object.Id)).Returns(second_object);
                                };

        Because of = () => object_got = Repository.GetById(second_object.Id);

        It should_not_be_null = () => object_got.ShouldNotBeNull();
        It should_return_correct_instance = () => object_got.ShouldEqual(second_object);
    }
}
