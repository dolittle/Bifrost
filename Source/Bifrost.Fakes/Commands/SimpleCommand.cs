using System;
using Bifrost.Commands;

namespace Bifrost.Fakes.Commands
{
    public class SimpleCommand : ICommand
    {
        public SimpleCommand() : this(Guid.NewGuid())
        {
        }

        public SimpleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string SomeString { get; set; }

        public int SomeInt { get; set; }
    }
}
