using System;
using System.Threading;
using Bifrost.Time;
using Machine.Specifications;

namespace Bifrost.Specs.Time.for_SystemClock
{
    [Subject(typeof(SystemClock))]
    public class when_setting_the_current_system_time_explicitly_multitple_times
    {
        static readonly DateTime first_explicitly_set_datetime = new DateTime(2010, 4, 1);
        static readonly DateTime second_explicitly_set_datetime = new DateTime(2010, 3, 1);
        static readonly DateTime third_explicitly_set_datetime = new DateTime(2010, 2, 1);
        static DateTime current_time_before_explicit_set;
        static DateTime current_time_after_first_explicit_set;
        static DateTime current_time_after_second_explicit_set;
        static DateTime current_time_after_third_explicit_set;
        static DateTime current_time_after_wait;
        static DateTime current_time_after_first_explicit_set_removed;
        static DateTime current_time_after_second_explicit_set_removed;
        static DateTime current_time_after_third_explicit_set_removed;

        Because of = () =>
                         {
                             current_time_before_explicit_set = SystemClock.GetCurrentTime();

                             using (SystemClock.SetNowTo(first_explicitly_set_datetime))
                             {
                                 current_time_after_first_explicit_set = SystemClock.GetCurrentTime();
                                 Thread.Sleep(10);
                                 current_time_after_wait = SystemClock.GetCurrentTime();
                                 using(SystemClock.SetNowTo(second_explicitly_set_datetime))
                                 {
                                     current_time_after_second_explicit_set = SystemClock.GetCurrentTime();
                                     using (SystemClock.SetNowTo(third_explicitly_set_datetime))
                                     {
                                         current_time_after_third_explicit_set = SystemClock.GetCurrentTime();
                                     }
                                     current_time_after_third_explicit_set_removed = SystemClock.GetCurrentTime();
                                 }
                                 current_time_after_second_explicit_set_removed = SystemClock.GetCurrentTime();
                             }
                             current_time_after_first_explicit_set_removed = SystemClock.GetCurrentTime();
                         };

        It should_return_the_actual_system_before_being_set = () => current_time_before_explicit_set.ShouldBeGreaterThan(first_explicitly_set_datetime);
        It should_return_the_explicitly_set_system_time_for_the_correct_scope_on_each_request = () =>
                                                                                                    {
                                                                                                        current_time_after_first_explicit_set.ShouldEqual(first_explicitly_set_datetime);
                                                                                                        current_time_after_wait.ShouldEqual(first_explicitly_set_datetime);
                                                                                                        current_time_after_second_explicit_set.ShouldEqual(second_explicitly_set_datetime);
                                                                                                        current_time_after_third_explicit_set.ShouldEqual(third_explicitly_set_datetime);
                                                                                                    };
        It should_revert_to_the_date_set_in_the_previous_scope_when_existing_a_using_block = () =>
                                                                                                 {
                                                                                                     current_time_after_third_explicit_set_removed.ShouldEqual(second_explicitly_set_datetime);
                                                                                                     current_time_after_second_explicit_set_removed.ShouldEqual(first_explicitly_set_datetime);
                                                                                                 };
        It should_revert_to_the_current_system_date_time_when_all_scopes_have_exited = () => current_time_after_first_explicit_set_removed.ShouldBeGreaterThan(current_time_before_explicit_set);

    }
}