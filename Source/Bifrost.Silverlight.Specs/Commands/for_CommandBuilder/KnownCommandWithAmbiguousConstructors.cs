using Bifrost.Commands;
namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class KnownCommandWithAmbiguousConstructors : Command
    {
        public KnownCommandWithAmbiguousConstructors(string something)
        {
            Something = something;
        }

        public KnownCommandWithAmbiguousConstructors(string something, string somethingElse)
        {
            Something = something;
        }

        public string Something { get; set; }
    }
}
