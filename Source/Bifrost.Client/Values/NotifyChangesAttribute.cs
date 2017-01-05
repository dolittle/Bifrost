/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.ComponentModel;

namespace Bifrost.Values
{
    /// <summary>
    /// Tells any weaver to implement <see cref="INotifyPropertyChanged"/> and 
    /// make any public properties notify of their changes
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NotifyChangesAttribute : Attribute
    {
    }
}
