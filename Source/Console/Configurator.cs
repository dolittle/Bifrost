using System.IO;
using Bifrost.Configuration;

namespace ConsoleApplication
{
    public class Configurator : ICanConfigure
    {
        public void Configure(IConfigure configure)
        {
            var currentDir = Directory.GetCurrentDirectory();

            var eventsPath = $"{currentDir}/Console/Events";

            configure
                .Serialization
                    .UsingJson()
                .Events
                    .UsingFiles(eventsPath);
        }
    }
}