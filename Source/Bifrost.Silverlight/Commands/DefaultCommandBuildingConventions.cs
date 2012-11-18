namespace Bifrost.Commands
{
    public class DefaultCommandBuildingConventions
    {
        public static CommandNameConvention TrimCommandFromName = (n) =>
        {
            if (n.EndsWith("Command"))
                return n.Substring(0,n.IndexOf("Command"));
            return n;
        };
    }
}
