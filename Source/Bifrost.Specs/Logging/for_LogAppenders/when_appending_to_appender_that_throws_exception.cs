using System;
using Bifrost.Logging;
using Machine.Specifications;

namespace Bifrost.Specs.Logging.for_LogAppenders
{
    public class when_appending_to_appender_that_throws_exception : given.one_appender
    {
        const string message = "Some message";
        const string file = "Some file";
        const int line_number = 42;
        const string member = "Some member";
        static Exception exception = new NotImplementedException();

        static Exception result;

        Establish context = () => appender.Setup(a => a.Append(file, line_number, member, LogLevel.Error, message, exception)).Callback(() => throw new NotImplementedException());

        Because of = () => result = Catch.Exception(() => appenders.Append(file, line_number, member, LogLevel.Error, message, exception));

        It should_swallow_exception = () => result.ShouldBeNull();
    }
}
