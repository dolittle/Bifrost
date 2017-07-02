using System.Security.Claims;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_UserSecurityActor
{
    public class when_checking_for_role_that_sits_as_part_of_role_claims : given.a_user_security_actor
    {
        const string first_role = "First Role";
        const string second_role = "Secnod Role";
        const string third_role = "Third Role";

        static bool result;

        Establish context = () => 
        {
            identity.AddClaims(new[] {
                new Claim(ClaimTypes.Role, first_role),
                new Claim(ClaimTypes.Role, second_role),
                new Claim(ClaimTypes.Role, third_role)
            });
        };

        Because of = () => result = actor.IsInRole(second_role);

        It should_be_considered_in_role = () => result.ShouldBeTrue();
    }
}