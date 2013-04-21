using System;
using System.Threading;
using Bifrost.Time;
using Machine.Specifications;

namespace Bifrost.Specs.Time.for_SystemClock
{
    [Subject(typeof(SystemClock))]
    public class when_setting_the_current_system_time_explicitly
    {
        static readonly DateTime explicitly_set_datetime = new DateTime(2010, 1, 1);
        static DateTime current_time_before_explicit_set;
        static DateTime current_time;
        static DateTime current_time_after_wait;
        static DateTime current_time_after_explicit_set_removed;

        Because of = () =>
                         {

                             current_time_before_explicit_set = SystemClock.GetCurrentTime();

                             using (SystemClock.SetNowTo(explicitly_set_datetime))
                             {
                                 current_time = SystemClock.GetCurrentTime();
                                 Thread.Sleep(10);
                                 current_time_after_wait = SystemClock.GetCurrentTime();
                             }

                             current_time_after_explicit_set_removed = SystemClock.GetCurrentTime();
                         };

        It should_return_the_actual_system_before_being_set = () => current_time_before_explicit_set.ShouldBeGreaterThan(explicitly_set_datetime);
        It should_return_the_explicitly_set_system_time_on_each_request = () =>
                                                                        {
                                                                            current_time.ShouldEqual(explicitly_set_datetime);
                                                                            current_time_after_wait.ShouldEqual(explicitly_set_datetime);
                                                                        };
        It should_revert_to_the_actual_system_date_outside_using_block = () => current_time_after_explicit_set_removed.ShouldBeGreaterThan(explicitly_set_datetime);

    }
}
