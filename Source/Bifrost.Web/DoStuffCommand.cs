using System;
using Bifrost.Commands;

namespace Bifrost.Web
{
    public class DoStuffCommand : ICommand
    {
        public Guid Id { get; set; }
        public string StringParameter { get; set; }
        public int IntParameter { get; set; }
    }
}