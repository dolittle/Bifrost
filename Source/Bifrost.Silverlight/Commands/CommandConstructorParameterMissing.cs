using System;

namespace Bifrost.Commands
{
    public class CommandConstructorParameterMissing : ArgumentException
    {
        public CommandConstructorParameterMissing(Type command) 
            : base(string.Format("Missing constructor parameter for command of type {0} during building", command.Name))
        {
        }
    }
}
