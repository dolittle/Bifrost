using Bifrost.Rules;
using Machine.Specifications;

namespace Bifrost.Specs.Rules.for_BrokenRuleReason
{
    public class when_creating
    {
        static string id = "3847286b-b508-4738-8975-f08383999f5a";
        static BrokenRuleReason reason;

        Because of = () => reason = BrokenRuleReason.Create(id);

        It should_return_an_instance_with_the_id_set = () => reason.Id.ToString().ToLowerInvariant().ShouldEqual(id);
    }
}
