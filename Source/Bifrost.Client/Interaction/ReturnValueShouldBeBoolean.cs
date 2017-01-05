/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Interaction
{
    public class ReturnValueShouldBeBoolean : ArgumentException
    {
        public ReturnValueShouldBeBoolean(string memberName, Type type)
            : base(string.Format("Method '{0}' on '{1}' must be return a boolean to be valid for canExecute checks", memberName, type.AssemblyQualifiedName)) { }
    }
}
