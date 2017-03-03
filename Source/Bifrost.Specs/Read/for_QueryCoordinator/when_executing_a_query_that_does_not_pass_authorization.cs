using Bifrost.Read;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Bifrost.Specs.Read.for_QueryCoordinator
{
    public class when_executing_a_query_that_does_not_pass_authorization : given.a_query_coordinator
    {
        static IQuery query;
        static PagingInfo paging;
        static QueryResult result;

        Establish   context = () => 
        {
            query = new QueryForKnownProvider();
            paging = new PagingInfo();

            var authorizationResult = new AuthorizationResult();
            var authorizeDescriptorResult = new AuthorizeDescriptorResult();
            var authorizeActionResult = new AuthorizeActionResult(new Fetching());
            var authorizeTargetResult = new AuthorizeTargetResult(new FetchingSecurityTarget());
            var authorizeSecurableResult = new AuthorizeSecurableResult(new Securable("Something"));
            var authorizeActorResult = new AuthorizeActorResult(new SecurityActor("Something"));
            authorizeActorResult.AddBrokenRule(new RoleRule(Mock.Of<IUserSecurityActor>(), "SomeRole"));
            authorizeSecurableResult.ProcessAuthorizeActorResult(authorizeActorResult);
            authorizeTargetResult.ProcessAuthorizeSecurableResult(authorizeSecurableResult);
            authorizeActionResult.ProcessAuthorizeTargetResult(authorizeTargetResult);
            authorizeDescriptorResult.ProcessAuthorizeActionResult(authorizeActionResult);
            authorizationResult.ProcessAuthorizeDescriptorResult(authorizeDescriptorResult);

            fetching_security_manager_mock.Setup(f => f.Authorize(Moq.It.IsAny<IQuery>())).Returns(authorizationResult);   
        };

        Because of = () => result = coordinator.Execute(query, paging);

        It should_not_pass_security = () => result.PassedSecurity.ShouldBeFalse();
        It should_have_hold_an_empty_items_array = () => result.Items.ShouldBeEmpty();
    }
}
