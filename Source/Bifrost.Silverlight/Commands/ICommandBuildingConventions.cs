namespace Bifrost.Commands
{
    public interface ICommandBuildingConventions
    {
        CommandNameConvention CommandName { get; set; }
        CommandConstructorParameterConvention CommandConstructorName { get; set; }
    }
}
