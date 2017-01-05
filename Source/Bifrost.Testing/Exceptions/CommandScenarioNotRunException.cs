/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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