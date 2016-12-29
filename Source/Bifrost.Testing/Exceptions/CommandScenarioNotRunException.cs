using System;

namespace Bifrost.Testing.Exceptions
{
    public class CommandScenarioNotRunException : Exception
    {
        public CommandScenarioNotRunException()
        {
        }

        public CommandScenarioNotRunException(string message)
            : base(message)
        {
        }

        public CommandScenarioNotRunException(string message, Exception inner)
            : base(message, inner)
        {
        }
   }
}