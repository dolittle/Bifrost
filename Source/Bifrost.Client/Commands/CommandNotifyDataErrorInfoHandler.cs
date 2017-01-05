/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.ComponentModel;

namespace Bifrost.Commands
{
    /// <summary>
    /// Represents an implementation of <see cref="INotifyDataErrorInfo"/>
    /// that will handle any invocations from an interceptor
    /// </summary>
    public class CommandNotifyDataErrorInfoHandler : INotifyDataErrorInfo
    {
#pragma warning disable 1591 // Xml Comments
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = (s, e) => { };

        public IEnumerable GetErrors(string propertyName)
        {
            return new string[0];
        }

        public bool HasErrors
        {
            get { return false; }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
