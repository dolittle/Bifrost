using System;
using Bifrost.Commands;

namespace Bifrost.SomeRandomNamespace
{
    public class CommandInADifferentNamespace : ICommand
    {
        public Guid Id { get; set; }
    }
}