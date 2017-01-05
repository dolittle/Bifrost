/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Interaction
{
    public class MissingMethodForCommand : ArgumentException
    {
        public MissingMethodForCommand(Type type, string methodName) :
            base(string.Format("Missing method '{0}' on '{1}'", methodName, type.AssemblyQualifiedName)) { }
    }
}
