using System.Collections.Generic;
using System.Dynamic;
using Bifrost.Execution;
using Bifrost.Tenancy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Tenancy.for_TenantPopulator
{
    public class when_populating_and_there_are_two_populators
    {
        static TenantPopulator populator;

        static Mock<ICanPopulateTenant> first_populator;
        static Mock<ICanPopulateTenant> second_populator;
        static ITenant tenant;
        static ExpandoObject details;

        Establish context = () =>
        {
            first_populator = new Mock<ICanPopulateTenant>();
            second_populator = new Mock<ICanPopulateTenant>();

            tenant = Mock.Of<ITenant>();
            details = new ExpandoObject();

            var populators = new Mock<IInstancesOf<ICanPopulateTenant>>();
            populators.Setup(p => p.GetEnumerator()).Returns(new List<ICanPopulateTenant>(
                new[]
                {
                    first_populator.Object,
                    second_populator.Object
                }).GetEnumerator());

            populator = new TenantPopulator(populators.Object);
        };

        Because of = () => populator.Populate(tenant, details);

        It should_ask_first_populator_to_populate = () => first_populator.Verify(p => p.Populate(tenant, details), Times.Once());
        It should_ask_second_populator_to_populate = () => second_populator.Verify(p => p.Populate(tenant, details), Times.Once());
    }
}
