/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Bifrost.Execution
{

    /// <summary>
    /// Represents a <see cref="IDispatcher"/>
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        System.Windows.Threading.Dispatcher _systemDispatcher;

        /// <summary>
        /// Initializes a new instance of <see cref="Dispatcher"/>
        /// </summary>
        /// <param name="systemDispatcher"></param>
        public Dispatcher(System.Windows.Threading.Dispatcher systemDispatcher)
        {
            _systemDispatcher = systemDispatcher;
        }

#pragma warning disable 1591 // Xml Comments
        public bool CheckAccess()
        {
            return _systemDispatcher.CheckAccess();
        }

        public void BeginInvoke(Delegate del, params object[] arguments)
        {
            _systemDispatcher.BeginInvoke(del, arguments);
        }

        public void BeginInvoke(Action action)
        {
            _systemDispatcher.BeginInvoke(action);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
