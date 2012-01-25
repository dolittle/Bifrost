using System;

namespace Bifrost.Services.Commands
{
    public class UnknownCommandException : ArgumentException
    {
        public UnknownCommandException(string commandName) : base(string.Format("Couldn't resolve '{0}' as command")) {}
    }
}
