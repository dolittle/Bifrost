using Bifrost.Diagnostics;
using Machine.Specifications;

namespace Bifrost.Specs.Diagnostics.for_Problems
{
    public class when_asking_if_has_any_problems_without_any
    {
        static Problems  problems;
        static bool result;

        Establish context = () => problems = new Problems();

        Because of = () => result = problems.Any;

        It should_not_have_any = () => result.ShouldBeFalse();
    }
}
