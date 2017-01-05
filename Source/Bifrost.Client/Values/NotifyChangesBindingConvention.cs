/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.ComponentModel;
using Bifrost.Execution;
using IContainer = Bifrost.Execution.IContainer;

namespace Bifrost.Values
{
    /// <summary>
    /// Represents a <see cref="IBindingConvention"/> that will associate any
    /// type adorned with the <see cref="NotifyChangesAttribute"/> with a 
    /// proxy type implementing <see cref="INotifyPropertyChanged"/> using
    /// the <see cref="NotifyingProxyWeaver"/>
    /// </summary>
    public class NotifyChangesBindingConvention : IBindingConvention
    {
        NotifyingObjectWeaver _weaver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyChangesBindingConvention"/>
        /// </summary>
        public NotifyChangesBindingConvention()
        {
            _weaver = new NotifyingObjectWeaver();
        }


#pragma warning disable 1591 // Xml Comments
        public bool CanResolve(IContainer container, Type service)
        {
            if (service.Name.Contains("ViewModel"))
            {
                var i = 0;
                i++;
            }
            return service.GetCustomAttributes(typeof(NotifyChangesAttribute), false).Length == 1;
        }

        public void Resolve(IContainer container, Type service)
        {
            container.Bind(service, _weaver.GetProxyType(service));
        }
#pragma warning restore 1591 // Xml Comments
    }
}
