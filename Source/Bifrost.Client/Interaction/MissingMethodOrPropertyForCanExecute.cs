/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Interaction
{
    public class MissingMethodOrPropertyForCanExecute : ArgumentException
    {
        public MissingMethodOrPropertyForCanExecute(string canExecuteWhen, Type type)
            : base(string.Format("Missing method or property called '{0}' on '{1}", canExecuteWhen, type.AssemblyQualifiedName)) { }
    }
}
