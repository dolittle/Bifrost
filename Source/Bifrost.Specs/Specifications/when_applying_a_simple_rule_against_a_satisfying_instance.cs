using System.Collections.Generic;
using System.Linq;
using Bifrost.Specifications;
using Bifrost.Specs.Specifications.given;
using Machine.Specifications;

namespace Bifrost.Specs.Specifications
{
    [Subject(typeof (Specification<>))]
    public class when_applying_a_simple_rule_against_a_satisfying_instance : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.IsSatisfiedBy(green_square);

        It should_be_satisfied = () => is_satisfied.ShouldBeTrue();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_a_simple_rule_against_a_non_satisfying_instance : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.IsSatisfiedBy(green_circle);

        It should_be_satisfied = () => is_satisfied.ShouldBeFalse();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_a_simple_rule_against_a_collection : given.rules_and_coloured_shapes
    {
        static IQueryable<ColouredShape> coloured_shapes;

        Because of = () => coloured_shapes = squares.SatisfyingElementsFrom(my_coloured_shapes);

        It should_contain_only_squares = () => coloured_shapes.All(r => r.Shape == "Square").ShouldBeTrue();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_a_not_rule_against_a_satisfying_instance : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = Is.Not(squares).IsSatisfiedBy(green_square);

        It should_be_not_satisfied = () => is_satisfied.ShouldBeFalse();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_a_not_rule_against_a_non_satisfying_instance : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = Is.Not(squares).IsSatisfiedBy(green_circle);

        It should_be_satisfied = () => is_satisfied.ShouldBeTrue();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_and_rule_against_a_satisfying_instance : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.And(green).IsSatisfiedBy(green_square);

        It should_be_satisfied = () => is_satisfied.ShouldBeTrue();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_and_rule_against_a_instance_satifying_only_one_part : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.And(green).IsSatisfiedBy(red_square);

        It should_not_be_satisfied = () => is_satisfied.ShouldBeFalse();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_and_rule_against_an_instance_satifying_no_parts : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.And(green).IsSatisfiedBy(red_circle);

        It should_not_be_satisfied = () => is_satisfied.ShouldBeFalse();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_or_rule_against_an_instance_satifying_no_parts : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.Or(green).IsSatisfiedBy(red_circle);

        It should_not_be_satisfied = () => is_satisfied.ShouldBeFalse();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_or_rule_against_an_instance_satifying_only_one_part : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.Or(green).IsSatisfiedBy(red_square);

        It should_be_satisfied = () => is_satisfied.ShouldBeTrue();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_or_rule_against_an_instance_satifying_both_parts : given.rules_and_coloured_shapes
    {
        static bool is_satisfied;

        Because of = () => is_satisfied = squares.Or(green).IsSatisfiedBy(green_square);

        It should_be_satisfied = () => is_satisfied.ShouldBeTrue();
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_or_rule_against_a_collection : given.rules_and_coloured_shapes
    {
        static IQueryable<ColouredShape> satisfied_shapes;
        static IEnumerable<ColouredShape> the_greens;
        static IEnumerable<ColouredShape> the_squares;
        static IEnumerable<ColouredShape> green_or_squares;

        Establish context = () =>
            {
                the_greens = my_coloured_shapes.Where(s => s.Colour == "Green").AsEnumerable();
                the_squares = my_coloured_shapes.Where(s => s.Shape == "Square").AsEnumerable();

                green_or_squares = the_greens.Union(the_squares).Distinct();

            };

        Because of = () => satisfied_shapes = squares.Or(green).SatisfyingElementsFrom(my_coloured_shapes);

        It should_have_all_instances_satisfying_either_part = () => satisfied_shapes.ShouldContainOnly(green_or_squares);
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_an_and_rule_against_a_collection : given.rules_and_coloured_shapes
    {
        static IQueryable<ColouredShape> satisfied_shapes;
        static IEnumerable<ColouredShape> the_greens;
        static IEnumerable<ColouredShape> the_squares;
        static IEnumerable<ColouredShape> green_squares;

        Establish context = () =>
        {
            the_greens = my_coloured_shapes.Where(s => s.Colour == "Green").AsEnumerable();
            the_squares = my_coloured_shapes.Where(s => s.Shape == "Square").AsEnumerable();

            green_squares = the_greens.Intersect(the_squares).Distinct();

        };

        Because of = () => satisfied_shapes = squares.And(green).SatisfyingElementsFrom(my_coloured_shapes);

        It should_have_all_instances_satisfying_both_parts = () => satisfied_shapes.ShouldContainOnly(green_squares);
    }

    [Subject(typeof(Specification<>))]
    public class when_applying_a_not_rule_against_a_collection : given.rules_and_coloured_shapes
    {
        static IQueryable<ColouredShape> satisfied_shapes;
        static IEnumerable<ColouredShape> the_not_greens;

        Establish context = () =>
        {
            the_not_greens = my_coloured_shapes.Where(s => s.Colour != "Green").AsEnumerable();
        };

        Because of = () => satisfied_shapes = Is.Not(green).SatisfyingElementsFrom(my_coloured_shapes);

        It should_have_all_instances_not_satisfying_the_rule = () => satisfied_shapes.ShouldContainOnly(the_not_greens);
    }
}