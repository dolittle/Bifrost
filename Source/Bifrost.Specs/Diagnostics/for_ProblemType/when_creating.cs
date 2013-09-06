using Bifrost.Diagnostics;
using Machine.Specifications;

namespace Bifrost.Specs.Diagnostics.for_ProblemType
{
    public class when_creating
    {
        const string id = "283a358b-906c-4af8-9ca1-d2eedb3723ac";
        const string description = "This is a problem";

        static ProblemType result;

        Because of = () => result = ProblemType.Create(id, description, ProblemSeverity.Warning);

        It should_hold_the_id = () => result.Id.ToString().ShouldEqual(id);
        It should_hold_the_description = () => result.Description.ShouldEqual(description);
        It should_hold_the_severity = () => result.Severity.ShouldEqual(ProblemSeverity.Warning);
    }
}
