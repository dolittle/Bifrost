using Bifrost.Logging;

namespace SimpleWeb
{
    public class LogAppendersConfigurator : ICanConfigureLogAppenders
    {
        public void Configure(ILogAppenders logAppenders)
        {
            var i = 0;
            i++;
        }
    }
}
