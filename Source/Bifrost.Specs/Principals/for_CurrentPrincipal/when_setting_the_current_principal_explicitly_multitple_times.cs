using System.Security.Principal;
using System.Threading;
using Machine.Specifications;

namespace Bifrost.Specs.Principals.for_CurrentPrincipal
{
    [Subject(typeof(Principal.CurrentPrincipal))]
    public class when_setting_the_current_principal_explicitly_multitple_times
    {
        static readonly IPrincipal first_explicitly_set_principal = new GenericPrincipal(new GenericIdentity(string.Empty),new string[0] );
        static readonly IPrincipal second_explicitly_set_principal = new GenericPrincipal(new GenericIdentity(string.Empty), new string[0]);
        static readonly IPrincipal third_explicitly_set_principal = new GenericPrincipal(new GenericIdentity(string.Empty), new string[0]);
        static IPrincipal current_principal_before_explicit_set;
        static IPrincipal current_principal_after_first_explicit_set;
        static IPrincipal current_principal_after_second_explicit_set;
        static IPrincipal current_principal_after_third_explicit_set;
        static IPrincipal current_principal_after_first_explicit_set_removed;
        static IPrincipal current_principal_after_second_explicit_set_removed;
        static IPrincipal current_principal_after_third_explicit_set_removed;

        Because of = () =>
                         {
                             current_principal_before_explicit_set = Principal.CurrentPrincipal.Get();

                             using (Principal.CurrentPrincipal.SetPrincipalTo(first_explicitly_set_principal))
                             {
                                 current_principal_after_first_explicit_set = Principal.CurrentPrincipal.Get();
                                 using (Principal.CurrentPrincipal.SetPrincipalTo(second_explicitly_set_principal))
                                 {
                                     current_principal_after_second_explicit_set = Principal.CurrentPrincipal.Get();
                                     using (Principal.CurrentPrincipal.SetPrincipalTo(third_explicitly_set_principal))
                                     {
                                         current_principal_after_third_explicit_set = Principal.CurrentPrincipal.Get();
                                     }
                                     current_principal_after_third_explicit_set_removed = Principal.CurrentPrincipal.Get();
                                 }
                                 current_principal_after_second_explicit_set_removed = Principal.CurrentPrincipal.Get();
                             }
                             current_principal_after_first_explicit_set_removed = Principal.CurrentPrincipal.Get();
                         };

        It should_return_the_actual_principal_before_being_set = () => current_principal_before_explicit_set.ShouldEqual(Thread.CurrentPrincipal);
        It should_return_the_explicitly_set_principal_for_the_correct_scope_on_each_request = () =>
                                                                                                    {
                                                                                                        current_principal_after_first_explicit_set.ShouldEqual(first_explicitly_set_principal);
                                                                                                        current_principal_after_second_explicit_set.ShouldEqual(second_explicitly_set_principal);
                                                                                                        current_principal_after_third_explicit_set.ShouldEqual(third_explicitly_set_principal);
                                                                                                    };
        It should_revert_to_the_principal_set_in_the_previous_scope_when_existing_a_using_block = () =>
                                                                                                 {
                                                                                                     current_principal_after_third_explicit_set_removed.ShouldEqual(second_explicitly_set_principal);
                                                                                                     current_principal_after_second_explicit_set_removed.ShouldEqual(first_explicitly_set_principal);
                                                                                                 };
        It should_revert_to_the_current_thread_principal_when_all_scopes_have_exited = () => current_principal_after_first_explicit_set_removed.ShouldEqual(Thread.CurrentPrincipal);

    }
}