using System;
using Bifrost.Commands;

namespace Bifrost.Specs.Commands.Diagnostics.for_CommandInheritanceRule
{
    public class CommandImplentingICommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
