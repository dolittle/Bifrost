using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Applications.for_ApplicationResourceIdentifier
{
    public class when_comparing_two_instances_representing_different_resource : given.different_identifiers
    {
        static bool result;

        Because of = () => result = identifier_a == identifier_b;

        It should_not_be_considered_the_same = () => result.ShouldBeFalse();
    }
}
