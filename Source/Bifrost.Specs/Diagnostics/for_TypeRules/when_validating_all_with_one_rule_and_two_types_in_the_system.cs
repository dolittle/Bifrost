using System;
using System.Collections.Generic;
using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Diagnostics.for_TypeRules
{
    public class when_validating_all_with_one_rule_and_two_types_in_the_system : given.type_rules_with_one_rule_and_two_types
    {
        static List<Type> types;

        Establish context = () =>
        {
            types = new List<Type>();
            type_rule_mock.Setup(t=>t.Validate(Moq.It.IsAny<Type>(), Moq.It.IsAny<IProblems>())).Callback(
                (Type type, IProblems problems) => types.Add(type));
        };

        Because of = () => type_rules.ValidateAll();

        It should_create_two_problems_instance = () => problems_factory_mock.Verify(p => p.Create(), Times.Exactly(2));
        It should_ask_rule_to_validate_first_rule = () => types[0].ShouldBeOfExactType(first_type);
        It should_ask_rule_to_validate_second_rule = () => types[1].ShouldBeOfExactType(first_type);
        It should_not_report_any_problems = () => problems_reporter_mock.Verify(r => r.Report(problems_mock.Object), Times.Never());
    }
}
