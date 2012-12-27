using Bifrost.Commands;
namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class KnownCommandWithConstructorParameter : Command
    {
        public KnownCommandWithConstructorParameter(string something)
        {
            Something = something;
        }

        public string Something { get; set; }
    }
}
