using Bifrost.Execution;

namespace Bifrost.Commands
{
    [Singleton]
    public class CommandBuildingConventions : ICommandBuildingConventions
    {
        public CommandBuildingConventions()
        {
            CommandName = DefaultCommandBuildingConventions.TrimCommandFromName;
        }

        public CommandNameConvention CommandName { get; set; }
    }
}
