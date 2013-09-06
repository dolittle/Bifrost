using System;
using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Diagnostics.for_TypeRules.given
{
    public class type_rules_with_one_rule_and_two_types : given.type_rules_with_one_rule
    {
        protected static Type first_type;
        protected static Type second_type;

        protected static Mock<IProblems> first_problems;
        protected static Mock<IProblems> second_problems;

        protected static IProblems[] all_problems;

        Establish context = () =>
        {
            first_type = typeof(object);
            second_type = typeof(string);

            first_problems = new Mock<IProblems>();
            second_problems = new Mock<IProblems>();

            all_problems = new[] {
                first_problems.Object,
                second_problems.Object
            };

            var counter = 0;
            problems_factory_mock.Setup(p => p.Create()).Returns(() => {
                var problems = all_problems[counter];
                counter++;
                return problems;
            });

            type_discoverer_mock.Setup(t => t.FindMultiple(type_for_rule)).Returns(new[] {
                    first_type, second_type
                });

            type_rules = new TypeRules(
                                type_discoverer_mock.Object,
                                container_mock.Object,
                                problems_factory_mock.Object,
                                problems_reporter_mock.Object
                             );
        };
    }
}
