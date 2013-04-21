using Bifrost.Entities;
using Bifrost.Views;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Views.for_View.given
{
    public class a_view_for<T>
    {
        protected static Mock<IEntityContext<T>> EntityContextMock;
        protected static View<T> Repository;

        Establish context = () =>
                                {
                                    EntityContextMock = new Mock<IEntityContext<T>>();
                                    Repository = new View<T>(EntityContextMock.Object);
                                };

    }
}
