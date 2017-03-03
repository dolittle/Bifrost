/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Values
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotifyChangesForAttribute : Attribute
    {
        public NotifyChangesForAttribute(params string[] propertyNames)
        {
            PropertyNames = propertyNames;
        }

        public string[] PropertyNames { get; private set; }
    }
}
