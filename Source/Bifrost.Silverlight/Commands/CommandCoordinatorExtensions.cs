
namespace Bifrost.Commands
{
    public static class CommandCoordinatorExtensions
    {
        public static Command Create(this ICommandCoordinator commandCoordinator, string name)
        {
            return Command.Create(commandCoordinator, name);
        }

    }
}
