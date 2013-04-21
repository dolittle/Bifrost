using System.Security.Principal;
using System.Threading;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_ExecutionContextManager
{
    public class when_Getting_current_and_identity_has_been_set_on_current_thread : given.given_an_execution_context_manager
    {
        const string    user_name = "Some user";
        static IExecutionContext execution_context;
        
        Establish context = () => {
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user_name), new string[0]);
            manager.Reset();
        };

        Because of = () => execution_context = manager.Current;

        It should_have_an_identity = () => execution_context.Identity.ShouldNotBeNull();
        It should_have_an_identity_with_correct_name = () => execution_context.Identity.Name.ShouldEqual(user_name);
    }
}
