using Bifrost.Validation.MetaData;
using Machine.Specifications;

namespace Bifrost.Specs.Validation.for_ValidationMetaData
{
    public class when_getting_for_type_with_two_generators : given.validation_meta_data_with_two_generators
    {
        const string property_with_multiple_rules = "bothGenerators";
        const string property_only_from_first_generator = "firstGenerator";
        const string property_only_from_second_generator = "secondGenerator";

        const string first_ruleset = "RuleSet1";
        const string second_ruleset = "RuleSet2";

        static TypeMetaData result;

        static TypeMetaData first_generator_meta_data;
        static TypeMetaData second_generator_meta_data;
        static Rule first_generator_rule_for_property_with_multiple_rules;
        static Rule second_generator_rule_for_property_with_multiple_rules;
        static Rule first_generator_rule;
        static Rule second_generator_rule;

        Establish context = () =>
        {
            first_generator_meta_data = new TypeMetaData();
            first_generator.type_meta_data_to_return = first_generator_meta_data;

            first_generator_rule = new Rule();
            first_generator_meta_data[property_only_from_first_generator][first_ruleset] = first_generator_rule;

            first_generator_rule_for_property_with_multiple_rules = new Rule();
            first_generator_meta_data[property_with_multiple_rules][first_ruleset] = first_generator_rule_for_property_with_multiple_rules;

            second_generator_meta_data = new TypeMetaData();
            second_generator.type_meta_data_to_return = second_generator_meta_data;

            second_generator_rule = new Rule();
            second_generator_meta_data[property_only_from_second_generator][first_ruleset] = second_generator_rule;

            second_generator_rule_for_property_with_multiple_rules = new Rule();
            second_generator_meta_data[property_with_multiple_rules][second_ruleset] = second_generator_rule_for_property_with_multiple_rules;
        };

        Because of = () => result = validation_meta_data.GetMetaDataFor(typeof(object));

        It should_return_meta_data = () => result.ShouldNotBeNull();

        It should_hold_first_generator_rule = () => result[property_only_from_first_generator][first_ruleset].ShouldEqual(first_generator_rule);
        It should_hold_first_generator_rule_for_property_expecting_multiple = () => result[property_with_multiple_rules][first_ruleset].ShouldEqual(first_generator_rule_for_property_with_multiple_rules);

        It should_hold_second_generator_rule = () => result[property_only_from_first_generator][first_ruleset].ShouldEqual(first_generator_rule);
        It should_hold_second_generator_rule_for_property_expecting_multiple = () => result[property_with_multiple_rules][second_ruleset].ShouldEqual(second_generator_rule_for_property_with_multiple_rules);
    }
}
