using Bifrost.Validation.MetaData;
using Machine.Specifications;
using Moq;
using Bifrost.Execution;
using System;

namespace Bifrost.Specs.Validation.MetaData.for_ValidationMetaDataGenerator.given
{
    public class a_validation_meta_data_generator_with_common_rules
    {
        protected static ValidationMetaDataGenerator generator;
        protected static Mock<IContainer> container_mock;
        protected static Mock<ITypeDiscoverer> type_discoverer_mock;

        Establish context = () =>
        {
            type_discoverer_mock = new Mock<ITypeDiscoverer>();
            type_discoverer_mock.Setup(t => t.FindMultiple<ICanGenerateRule>()).
                Returns(new[] {
                    typeof(RequiredGenerator),
                    typeof(EmailGenerator),
                    typeof(LessThanGenerator),
                    typeof(GreaterThanGenerator)
                });

            container_mock = new Mock<IContainer>();
            container_mock.Setup(c => c.Get(Moq.It.IsAny<Type>())).
                Returns((Type t) => Activator.CreateInstance(t));
            generator = new ValidationMetaDataGenerator(type_discoverer_mock.Object, container_mock.Object);
        };
    }
}
