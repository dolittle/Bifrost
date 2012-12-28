using Bifrost.Commands;
using System;

namespace Bifrost.Silverlight.Specs.Commands.for_CommandBuilder
{
    public class KnownCommand : Command
    {
        public KnownCommand()
        {
            Something = "HEllo world";
        }

        public string Something { get; set; }
        public int Integer { get; set; }
    }
}
