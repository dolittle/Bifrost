using System.Threading;
using Bifrost.Execution;
using Machine.Specifications;

namespace Bifrost.Specs.Execution.for_DefaultCallContext
{
    public class when_setting_data_from_different_threads
    {
        static DefaultCallContext call_context;

        static string data_from_other_thread;

        Establish context = () => call_context = new DefaultCallContext();

        Because of = () =>
        {
            call_context.SetData("Something", "Hello");

            var otherThread = new Thread(() =>
            {
                call_context.SetData("Something", "World");
                data_from_other_thread = call_context.GetData<string>("Something");
            });
            otherThread.Start();
            otherThread.Join();
        };

        It should_return_the_correct_data_for_default_thread = () =>
            call_context.GetData<string>("Something").ShouldEqual("Hello");

        It should_return_the_correct_data_for_the_other_thread = () =>
            data_from_other_thread.ShouldEqual("World");
    }
}
