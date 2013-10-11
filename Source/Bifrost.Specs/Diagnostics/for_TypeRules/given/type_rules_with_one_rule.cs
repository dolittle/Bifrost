using System;
using Bifrost.Diagnostics;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Diagnostics.for_TypeRules.given
{
    public class type_rules_with_one_rule : all_dependencies
    {
        protected static TypeRules type_rules;
        protected static Mock<ITypeRuleFor<object>> type_rule_mock;
        protected static Mock<IProblems> problems_mock;
        protected static Type type_for_rule;
        protected static Type rule_type;

        Establish context = () =>
        {
            type_rule_mock = new Mock<ITypeRuleFor<object>>();
            problems_mock = new Mock<IProblems>();

            type_for_rule = typeof(object);
            rule_type = type_rule_mock.Object.GetType();

            type_discoverer_mock.Setup(t => t.FindMultiple(typeof(ITypeRuleFor<>))).Returns(new[] {
                    rule_type
                });
            type_discoverer_mock.Setup(t => t.FindMultiple(type_for_rule)).Returns(new[] {
                    type_for_rule
                });

            container_mock.Setup(t => t.Get(rule_type)).Returns(type_rule_mock.Object);

            problems_factory_mock.Setup(p => p.Create()).Returns(problems_mock.Object);

            type_rules = new TypeRules(
                                type_discoverer_mock.Object,
                                container_mock.Object,
                                problems_factory_mock.Object,
                                problems_reporter_mock.Object
                             );
        };
    }
}
