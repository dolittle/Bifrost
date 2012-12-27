using Bifrost.Execution;

namespace Bifrost.Commands
{
    [Singleton]
    public class CommandBuildingConventions : ICommandBuildingConventions
    {
        public CommandBuildingConventions()
        {
            CommandName = DefaultCommandBuildingConventions.TrimCommandFromName;
            CommandConstructorName = DefaultCommandBuildingConventions.CamelCaseConstructorParameters;
        }

        public CommandNameConvention CommandName { get; set; }
        public CommandConstructorParameterConvention CommandConstructorName { get; set; }
    }
}
