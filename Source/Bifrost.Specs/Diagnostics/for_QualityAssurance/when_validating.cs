using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Diagnostics.for_QualityAssurance
{
    public class when_validating
    {
        static Mock<ITypeRules> type_rules_mock;
        static QualityAssurance quality_assurance;

        Establish context = () => 
        {
            type_rules_mock = new Mock<ITypeRules>();
            quality_assurance = new QualityAssurance(type_rules_mock.Object);
        };

        Because of = () => quality_assurance.Validate();

        It should_initiate_the_type_rules_system = () => type_rules_mock.Verify(t=>t.ValidateAll(), Times.Once());
    }
}
