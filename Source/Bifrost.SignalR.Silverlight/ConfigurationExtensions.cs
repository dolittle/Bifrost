using Bifrost.SignalR.Silverlight.Commands;

namespace Bifrost.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigure UsingSignalR(this ICommandsConfiguration commandsConfiguration)
        {
            commandsConfiguration.CommandCoordinatorType = typeof(CommandCoordinator);
            return Configure.Instance;
        }
    }
}
