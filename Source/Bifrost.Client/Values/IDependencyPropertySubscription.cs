/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.ComponentModel;

namespace Bifrost.Values
{
    public interface IDependencyPropertySubscription : INotifyPropertyChanged
    {
        object Value { get; set; }
    }
}
